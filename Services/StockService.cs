using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services;

public class StockService : IStockService
{
    private readonly List<BuyOrder> _buyOrders;
    private readonly List<SellOrder> _sellOrders;

    public StockService()
    {
        _buyOrders = new List<BuyOrder>();
        _sellOrders = new List<SellOrder>();
    }
    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        // Validation: buyOrderRequest can't be null
        if (buyOrderRequest == null)
            throw new ArgumentException(nameof(buyOrderRequest));
        
        // Model validate
        ValidationHelper.ModelValidation(buyOrderRequest);
        
        // Convert buyOrderRequest into BuyOrder type
        BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
        
        // Generate BuyOrderID
        buyOrder.BuyOrderID = Guid.NewGuid();
        
        // Add buyOrder object to buy orders list
        _buyOrders.Add(buyOrder);

        // Convert the BuyOrder object into BuyOrderResponse type
        return buyOrder.ToBuyOrderResponse();
    }

    public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
    {
        return _stockServiceImplementation.CreateSellOrder(sellOrderRequest);
    }

    public List<BuyOrderResponse> GetBuyOrders()
    {
        return _stockServiceImplementation.GetBuyOrders();
    }

    public List<SellOrderResponse> GetSellOrders()
    {
        return _stockServiceImplementation.GetSellOrders();
    }
}