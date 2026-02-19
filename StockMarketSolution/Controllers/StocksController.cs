using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockMarketSolution.Models;

namespace StockMarketSolution.Controllers;

[Route("[controller]")]
public class StocksController : Controller
{
    private readonly TradingOptions _tradingOptions;
    private readonly IFinnhubService _finnhubService;
    
    /// <summary>
    /// Constructor for TradeController that executes when a new object is created for the class
    /// </summary>
    /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
    /// <param name="finnhubService">Injecting FinnhubService</param>
    public StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService)
    {
        _tradingOptions = tradingOptions.Value;
        _finnhubService = finnhubService;
    }

    [Route("/")]
    [Route("[action]/{stock?}")]
    [Route("~/[action]/{stock?}")]
    public async Task<IActionResult> Explore(string? stock, bool showAll = false)
    {
        // Get company profile from API server
        List<Dictionary<string, string>>? stockDictionary = await _finnhubService.GetStocks();

        List<Stock> stocks = new List<Stock>();

        if (stockDictionary is not null)
        {
            // Filter stocks
            if (!showAll && _tradingOptions.Top25PopularStocks != null)
            {
                string[]? top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                
                if (top25PopularStocksList is not null)
                {
                    stockDictionary = stockDictionary
                        .Where(temp => top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
                        .ToList();
                }
            }
            
            // Convert dictionary objects into Stock objects
            stocks = stockDictionary
                .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
                .ToList();
        }

        ViewBag.stock = stock;
        return View(stocks);
    }
    
    
}