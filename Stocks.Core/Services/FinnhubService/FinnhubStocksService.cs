using System.Text.Json;
using Exceptions;
using ServiceContracts;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;


namespace Services;

public class FinnhubStocksService : IFinnhubStocksService
{

    private readonly IFinnhubRepository _finnhubRepository;

    public FinnhubStocksService(IFinnhubRepository finnhubRepository)
    {
        _finnhubRepository = finnhubRepository;
    }

    public async Task<List<Dictionary<string, string>>?> GetStocks()
    {
        try
        {
            // Invoke repository
            List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();

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