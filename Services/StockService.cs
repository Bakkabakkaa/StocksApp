using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class StockService : IStockService
{
    private IStockService _stockServiceImplementation;
    public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
    {
        return _stockServiceImplementation.CreateBuyOrder(buyOrderRequest);
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