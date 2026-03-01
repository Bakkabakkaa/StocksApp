using Entities;

namespace RepositoryContracts;

/// <summary>
/// Represents Stocks service that includes operation like buy order, sell order
/// </summary>
public interface IStocksRepository
{
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <param name="buyOrder">Buy order object</param>
    /// <returns></returns>
    Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

    /// <summary>
    /// Creates a sell order
    /// </summary>
    /// <param name="sellOrder"></param>
    /// <returns></returns>
    Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

    /// <summary>
    /// Returns all existing buy orders
    /// </summary>
    /// <returns>Returns a list of objects of BuyOrder typ</returns>
    Task<List<BuyOrder>> GetBuyOrders();

    /// <summary>
    /// Returns all existing sell orders
    /// </summary>
    /// <returns>Returns a list of objects of SellOrder type</returns>
    Task<List<SellOrder>> GetSellOrders();


}