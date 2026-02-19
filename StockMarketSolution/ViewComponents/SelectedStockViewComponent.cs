using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace StockMarketSolution.ViewComponents;

public class SelectedStockViewComponent : ViewComponent
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
    public SelectedStockViewComponent(IOptions<TradingOptions> tradingOptions, IStockService stocksService, IFinnhubService finnhubService, IConfiguration configuration)
    {
        _tradingOptions = tradingOptions.Value;
        _stockService = stocksService;
        _finnhubService = finnhubService;
        _configuration = configuration;
    }

    public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
    {
        Dictionary<string, object>? companyProfileDictionary = null;

        if (stockSymbol != null)
        {
            companyProfileDictionary = await _finnhubService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stockPriceDictionary = await _finnhubService.GetStockPriceQuote(stockSymbol);
            if (stockPriceDictionary != null && companyProfileDictionary != null)
            {
                companyProfileDictionary.Add("price", stockPriceDictionary["c"]);
            }
        }
        
        if (companyProfileDictionary != null && companyProfileDictionary.ContainsKey("logo"))
            return View(companyProfileDictionary);
        else
            return Content("");
    }
}