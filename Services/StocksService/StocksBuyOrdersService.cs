using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services;

public class StocksBuyOrdersService : IBuyOrdersService
{
    private readonly IStocksRepository _stocksRepository;

    public StocksBuyOrdersService(IStocksRepository stocksRepository)
    {
        _stocksRepository = stocksRepository;
    }
    public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        // Validation: buyOrderRequest can't be null
        if (buyOrderRequest == null)
            throw new ArgumentNullException(nameof(buyOrderRequest));
        
        // Model validate
        ValidationHelper.ModelValidation(buyOrderRequest);
        
        // Convert buyOrderRequest into BuyOrder type
        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
        
        // Generate BuyOrderID
        buyOrder.BuyOrderID = Guid.NewGuid();
        
        // Add buyOrder object to buy orders list

        BuyOrder buyOrderFromRepo = await _stocksRepository.CreateBuyOrder(buyOrder);

        // Convert the BuyOrder object into BuyOrderResponse type
        return buyOrder.ToBuyOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
        // Convert all BuyOrder objects into BuyOrderResponse objects
        List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();

        return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
    }
}