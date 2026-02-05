using Entities;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class that represents a buy order to purchase the stocks -
/// that can be used as return type of Stocks service
/// </summary>
public class BuyOrderResponse
{
    /// <summary>
    /// The unique ID of the buy order
    /// </summary>
    public Guid BuyOrderID { get; set; }


    /// <summary>
    /// The unique symbol of the stock
    /// </summary>
    public string StockSymbol { get; set; }


    /// <summary>
    /// The company name of the stock
    /// </summary>
    public string StockName { get; set; }


    /// <summary>
    /// Date and time of order, when it is placed by the user
    /// </summary>
    public DateTime DateAndTimeOfOrder { get; set; }


    /// <summary>
    /// The number of stocks (shares) to buy
    /// </summary>
    public uint Quantity { get; set; }


    /// <summary>
    /// The price of each stock (share)
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Total purchase amount
    /// </summary>
    public double TradeAmount { get; set; }
}

public static class BuyOrderExtensions
{
    /// <summary>
    /// An extensions method to convert an object of BuyOrder class into BuyOrderResponse class
    /// </summary>
    /// <param name="buyOrder">The BuyOrder object to convert</param>
    /// <returns>Returns the converted BuyOrderResponse object</returns>
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        return new BuyOrderResponse()
        {
            BuyOrderID = buyOrder.BuyOrderID, StockSymbol = buyOrder.StockSymbol,
            StockName = buyOrder.StockName, DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
            Price = buyOrder.Price, Quantity = buyOrder.Quantity,
            TradeAmount = buyOrder.Price * buyOrder.Quantity
        };
    }
}