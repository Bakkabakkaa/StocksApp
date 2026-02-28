using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services;

public class StocksSellOrdersService : ISellOrdersService
{
    private readonly IStocksRepository _stocksRepository;

    public StocksSellOrdersService(IStocksRepository stocksRepository)
    {
        _stocksRepository = stocksRepository;
    }

    public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        // Validation: sellOrderRequest can't be null
        if (sellOrderRequest == null)
            throw new ArgumentNullException(nameof(sellOrderRequest));
        
        // Model validation
        ValidationHelper.ModelValidation(sellOrderRequest);

        // Convert sellOrderRequest into SellOrder typ
        SellOrder sellOrder = sellOrderRequest.ToSellOrder();
        
        // Generate SellOrderID
        sellOrder.SellOrderID = Guid.NewGuid();
        
        // Add sell order object to sell orders list
        SellOrder SellOrderFromRepo = await _stocksRepository.CreateSellOrder(sellOrder);

        // Convert the SellOrder object into SellOrderResponse type
        return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
        // Convert all SellOrder objects into SellOrderResponse objects
        List<SellOrder> sellOrders = await _stocksRepository.GetSellOrders();

        return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
    }
}