using ServiceContracts.DTO;

namespace ServiceContracts;

/// <summary>
/// Represents Stocks service that includes
/// operations like buy order, sell order
/// </summary>
public interface IStockService
{
    /// <summary>
    /// Create a buy order
    /// </summary>
    /// <param name="buyOrderRequest">Buy order  object</param>
    /// <returns>Return buy order response</returns>
    BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
    
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <param name="sellOrderRequest">Sell order object</param>
    /// <returns>Return sell order response</returns>
    SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest);

    /// <summary>
    /// Returns all existing buy orders
    /// </summary>
    /// <returns>Returns a list of objects of BuyOrder type</returns>
    List<BuyOrderResponse> GetBuyOrders();

    /// <summary>
    /// Returns all existing sell orders
    /// </summary>
    /// <returns>Returns a list of objects of SellOrder type</returns>
    List<SellOrderResponse> GetSellOrders();

}