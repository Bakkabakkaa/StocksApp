using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using StockMarketSolution.Filters.ActionFilters;
using StockMarketSolution.Models;

namespace StockMarketSolution.Controllers;

[Route("[controller]")]
public class TradeController : Controller
{
    private readonly TradingOptions _tradingOptions;
    private readonly IStockService _stockService;
    private readonly IFinnhubService _finnhubService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TradeController> _logger;

    /// <summary>
    /// Constructor for TradeController that executes when a new object is created for the class
    /// </summary>
    /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
    /// <param name="stocksService">Injecting StocksService</param>
    /// <param name="finnhubService">Injecting FinnhubService</param>
    /// <param name="configuration">Injecting IConfiguration</param>
    /// <param name="logger">Injecting ILogger</param>
    public TradeController(IOptions<TradingOptions> tradingOptions, IStockService stockService,
        IFinnhubService finnhubService, IConfiguration configuration, ILogger<TradeController> logger)
    {
        _tradingOptions = tradingOptions.Value;
        _stockService = stockService;
        _finnhubService = finnhubService;
        _configuration = configuration;
        _logger = logger;
    }

    [Route("[action]/{stockSymbol}")]
    [Route("~/[controller]/{stockSymbol}")]
    public async Task<IActionResult> Index(string stockSymbol)
    {
        // Logger
        _logger.LogInformation("In TradeController.Index() action method");
        _logger.LogDebug("stockSymbol: {stockSymbol}", stockSymbol);
        
        // Reset stock symbol if not exists
        if (string.IsNullOrEmpty(stockSymbol))
            stockSymbol = "MSFT";
        
        // Get company profile from API server
        Dictionary<string, object>? companyProfileDictionary = await 
            _finnhubService.GetCompanyProfile(stockSymbol);
        
        // Get stock price quotes from API server
        Dictionary<string, object>? stockQuoteDictionary = await 
            _finnhubService.GetStockPriceQuote(stockSymbol);

        // Create model object
        StockTrade stockTrade = new StockTrade() { StockSymbol = stockSymbol };
        
        if (companyProfileDictionary != null && stockQuoteDictionary != null)
        {
            stockTrade = new StockTrade()
            {
                StockSymbol = companyProfileDictionary["ticker"].ToString(),
                StockName = companyProfileDictionary["name"].ToString(),
                Quantity = _tradingOptions.DefaultOrderQuantity ?? 0,
                Price = double.Parse(
                    stockQuoteDictionary["c"].ToString(),
                    CultureInfo.InvariantCulture
                )

            };
        }
        
        // Send Finnhub token to view
        ViewBag.FinnhubToken = _configuration["FinnhubToken"];

        return View(stockTrade);
    }

    [HttpPost]
    [Route("[action]")]
    [TypeFilter(typeof(CreateOrderActionFilter))]
    public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
    {
        // Invoke service method
        BuyOrderResponse buyOrderResponse = await _stockService.CreateBuyOrder(orderRequest);

        return RedirectToAction(nameof(Orders));
    }

    [HttpPost]
    [Route("[action]")]
    [TypeFilter(typeof(CreateOrderActionFilter))]
    public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
    {
        // Invoke service method
        SellOrderResponse sellOrderResponse = await _stockService.CreateSellOrder(orderRequest);

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

    [Route("[action]")]
    public async Task<IActionResult> OrdersPDF()
    {
        // Get list of orders
        List<IOrderResponse> orders = new List<IOrderResponse>();
        orders.AddRange(await _stockService.GetBuyOrders());
        orders.AddRange(await _stockService.GetSellOrders());

        orders = orders.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

        ViewBag.TradingOptions = _tradingOptions;
        
        // Return view as pdf
        return new ViewAsPdf("OrdersPDF", orders, ViewData)
        {
            PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
            PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
        };
    }
}