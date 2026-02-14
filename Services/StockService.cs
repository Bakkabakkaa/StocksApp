using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services;

public class StockService : IStockService
{
    private readonly StockMarketDbContext _db;

    public StockService(StockMarketDbContext stockMarketDbContext)
    {
        _db = stockMarketDbContext;
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
        _db.BuyOrders.Add(buyOrder);
        await _db.SaveChangesAsync();

        // Convert the BuyOrder object into BuyOrderResponse type
        return buyOrder.ToBuyOrderResponse();
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
        
        // Add sellOrder object to sell orders list
        _db.SellOrders.Add(sellOrder);
        await _db.SaveChangesAsync();

        // Convert the SellOrder object into SellOrderResponse type
        return sellOrder.ToSellOrderResponse();
    }

    public async Task<List<BuyOrderResponse>> GetBuyOrders()
    {
        // Convert all BuyOrder objects into BuyOrderResponse objects
        return await _db.BuyOrders
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .Select(temp => temp.ToBuyOrderResponse()).ToListAsync();
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
        // Convert all SellOrder objects into SellOrderResponse objects
        return await _db.SellOrders
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .Select(temp => temp.ToSellOrderResponse()).ToListAsync();
    }
}