using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class that represents a buy order to purchase
/// the stocks - that can be used while inserting / updating
/// </summary>
public class BuyOrderRequest
{
    /// <summary>
    /// The unique symbol of the stock
    /// </summary>
    [Required(ErrorMessage = "Stock Symbol can't be null or empty")]
    public string StockSymbol { get; set; }
    
    /// <summary>
    /// The company name of the stock
    /// </summary>
    [Required(ErrorMessage = "Stock Name can't be null or empty")]
    public string StockName { get; set; }
    
    /// <summary>
    /// Date and time order, when it is placed by user
    /// </summary>
    public DateTime DateAndTimeOfOrder { get; set; }
    
    /// <summary>
    /// The number of stocks (shares) to buy
    /// </summary>
    [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in single order. Minimum is 1.")]
    public uint Quantity { get; set; }
    
    /// <summary>
    /// The price of each stock (share)
    /// </summary>
    [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1.")]
    public double Price { get; set; }

    /// <summary>
    /// Converts the current object of BuyOrderRequest into a new object of BuyOrder type
    /// </summary>
    /// <returns></returns>
    public BuyOrder ToBuyOrder()
    {
        return new BuyOrder()
        {
            StockSymbol = this.StockSymbol, StockName = this.StockName,
            DateAndTimeOfOrder = this.DateAndTimeOfOrder, Price = this.Price,
            Quantity = this.Quantity
        };
    }
}