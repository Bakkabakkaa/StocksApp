using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StockMarketSolution.Models;

namespace StockMarketSolution.Controllers;

[Route("[controller]")]
public class TradeController : Controller
{
    private readonly TradingOptions _tradingOptions;
    private readonly IStockService _stockService;
    private readonly IFinnhubService _finnhubService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor for TradeController that executes when a new object is created for the class
    /// </summary>
    /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
    /// <param name="stocksService">Injecting StocksService</param>
    /// <param name="finnhubService">Injecting FinnhubService</param>
    /// <param name="configuration">Injecting IConfiguration</param>
    public TradeController(IOptions<TradingOptions> tradingOptions, IStockService stockService,
        IFinnhubService finnhubService, IConfiguration configuration)
    {
        _tradingOptions = tradingOptions.Value;
        _stockService = stockService;
        _finnhubService = finnhubService;
        _configuration = configuration;
    }

    [Route("/")]
    [Route("[action]")]
    [Route("~/[controller]")]
    public IActionResult Index()
    {
        // Reset stock symbol if not exists
        if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
            _tradingOptions.DefaultStockSymbol = "MSFT";
        
        // Get company profile from API server
        Dictionary<string, object>? companyProfileDictionary =
            _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
        
        // Get stock price quotes from API server
        Dictionary<string, object>? stockQuoteDictionary =
            _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

        // Create model object
        StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
        
        if (companyProfileDictionary != null && stockQuoteDictionary != null)
        {
            stockTrade = new StockTrade()
            {
                StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]),
                StockName = Convert.ToString(companyProfileDictionary["name"]),
                Price = ((JsonElement)stockQuoteDictionary["c"]).GetDouble()
            };
        }
        
        // Send Finnhub token to view
        ViewBag.FinnhubToken = _configuration["FinnhubToken"];

        return View(stockTrade);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
    {
        // Update date of order
        buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
        
        // Re-validate the model object after updating the date
        ModelState.Clear();
        TryValidateModel(buyOrderRequest);

        if (!ModelState.IsValid)
        {
            ViewBag.Errors = ModelState.Values.
                SelectMany(v => v.Errors).
                Select(e => e.ErrorMessage).ToList();

            StockTrade stockTrade = new StockTrade()
            {
                StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity,
                StockSymbol = buyOrderRequest.StockSymbol
            };

            return View("Index", stockTrade);
        }
        // Invoke service method
        BuyOrderResponse buyOrderResponse = await _stockService.CreateBuyOrder(buyOrderRequest);

        return RedirectToAction(nameof(Orders));
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
    {
        // Update date of order
        sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
        
        // Re-validate the model object after updating the date
        ModelState.Clear();
        TryValidateModel(sellOrderRequest);

        if (!ModelState.IsValid)
        {
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
            return View("Index", stockTrade);
        }
        
        // Invoke service method
        SellOrderResponse sellOrderResponse = await _stockService.CreateSellOrder(sellOrderRequest);

        return RedirectToAction(nameof(Orders));
    }

    [Route("[action]")]
    public async Task<IActionResult> Orders()
    {
        // Invoke service methods
        List<BuyOrderResponse> buyOrderResponses = await _stockService.GetBuyOrders();
        List<SellOrderResponse> sellOrderResponses = await _stockService.GetSellOrders();
        
        // Create model object
        Orders orders = new Orders()
        {
            BuyOrders = buyOrderResponses,
            SellOrders = sellOrderResponses
        };

        ViewBag.TradingOptions = _tradingOptions;

        return View(orders);
    }
}