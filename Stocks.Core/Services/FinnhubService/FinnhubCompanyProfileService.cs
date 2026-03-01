using System.Text.Json;
using Exceptions;
using ServiceContracts;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;


namespace Services;

public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
{

    private readonly IFinnhubRepository _finnhubRepository;

    public FinnhubCompanyProfileService(IFinnhubRepository finnhubRepository)
    {
        _finnhubRepository = finnhubRepository;
    }
    
    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
        try
        {
            // Invoke repository
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);
            
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