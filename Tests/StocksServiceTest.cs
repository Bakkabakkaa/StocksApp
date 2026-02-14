using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests;

public class StocksServiceTest
{
    private readonly IStockService _stockService;

    public StocksServiceTest()
    {
        _stockService = new StockService(new StockMarketDbContext(new DbContextOptionsBuilder<StockMarketDbContext>().Options));
    }

    #region CreateBuyOrder

    [Fact]
    public async Task CreateBuyOrder_NullBuyOrder_ToBeArgumentsNullException()
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = null;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public async Task CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Quantity = buyOrderQuantity;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(100001)]
    public async Task CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Quantity = buyOrderQuantity;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public async Task CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Price = buyOrderPrice;

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }
    
    [Theory]
    [InlineData(10001)]
    public async Task CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderPrice)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Price = buyOrderPrice;

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public async Task CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.StockSymbol = null;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }
    
    [Fact]
    public async Task CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31");
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public async Task CreateBuyOrder_ValidData_ToBeSuccessful()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        
        // Act
        BuyOrderResponse buyOrderResponse = await _stockService.CreateBuyOrder(buyOrderRequest);
        
        // Assert
        Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
    }


    #endregion

    #region CreateSellOrder

    [Fact]
    public async Task CreateSellOrder_NullSellOrder_ToBeArgumentNullException()
    {
        // Arrange
        SellOrderRequest? sellOrderRequest = null;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public async Task CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint sellOrderQuantity)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Quantity = sellOrderQuantity;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(100001)]
    public async Task CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Quantity = sellOrderQuantity;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public async Task CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint sellOrderPrice)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Price = sellOrderPrice;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(10001)]
    public async Task CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderPrice)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Price = sellOrderPrice;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public async Task CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.StockSymbol = null;
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public async Task CreateSellOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31");
        
        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public async Task CreateSellOrder_ValidData_ToBeSuccessful()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        
        // Act
        SellOrderResponse sellOrderResponse = await _stockService.CreateSellOrder(sellOrderRequest);
        
        // Assert
        Assert.NotEqual(Guid.Empty, sellOrderResponse.SellOrderID);
    }
    
    #endregion

    #region GetBuyOrders

    [Fact]
    public async Task GetAllBuyOrders_DefaultList_ToBeEmpty()
    {
        // Act
        List<BuyOrderResponse> buyOrderResponseList = await _stockService.GetBuyOrders();
        
        // Assert
        Assert.Empty(buyOrderResponseList);
    }

    [Fact]
    public async Task GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        BuyOrderResponse buyOrderResponseFromAdd = await _stockService.CreateBuyOrder(buyOrderRequest);
        
        // Act
        List<BuyOrderResponse> buyOrderResponseListFromGet = await _stockService.GetBuyOrders();
        
        // Assert
        Assert.Contains(buyOrderResponseFromAdd, buyOrderResponseListFromGet);
    }

    #endregion
    
    #region GetSellOrders

    [Fact]
    public async Task GetAllSellOrders_DefaultList_ToBeEmpty()
    {
        // Act
        List<SellOrderResponse> sellOrderResponses = await _stockService.GetSellOrders();
        
        // Assert
        Assert.Empty(sellOrderResponses);
    }

    [Fact]
    public async Task GetAllSellOrders_WithFewSellOrders_ToBeSuccessful()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        SellOrderResponse sellOrderResponseFromAdd = await _stockService.CreateSellOrder(sellOrderRequest);
        
        // Act
        List<SellOrderResponse> sellOrderResponseListFromGet = await _stockService.GetSellOrders();
        
        // Assert
        Assert.Contains(sellOrderResponseFromAdd, sellOrderResponseListFromGet);
    }

    #endregion
    
    

    private BuyOrderRequest CreateValidBuyOrderRequest()
    {
        return new BuyOrderRequest()
        {
            StockSymbol = "MSFT", StockName = "Microsoft",
            Price = 10, Quantity = 10,
            DateAndTimeOfOrder = new DateTime(2026, 2, 3)
        };
    }
    
    
    
    private SellOrderRequest CreateValidSellOrderRequest()
    {
        return new SellOrderRequest()
        {
            StockSymbol = "MSFT", StockName = "Microsoft",
            Price = 10, Quantity = 10,
            DateAndTimeOfOrder = new DateTime(2026, 2, 3)
        };
    }
}