using System.Text.Json;
using Exceptions;
using ServiceContracts;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;


namespace Services;

public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
{

    private readonly IFinnhubRepository _finnhubRepository;

    public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository)
    {
        _finnhubRepository = finnhubRepository;
    }
    
    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        try
        {
            // Invoke repository
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);

            // Return response dictionary back to the caller
            return responseDictionary;
        }
        catch (Exception ex)
        {
            FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", ex);
            throw finnhubException;
        }
    }
}