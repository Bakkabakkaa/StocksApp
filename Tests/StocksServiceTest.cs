using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests;

public class StocksServiceTest
{
    private readonly IStockService _stockService;

    public StocksServiceTest()
    {
        _stockService = new StockService();
    }

    #region CreateBuyOrder

    [Fact]
    public void CreateBuyOrder_NullBuyOrder_ToBeArgumentsNullException()
    {
        // Arrange
        BuyOrderRequest? buyOrderRequest = null;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Quantity = buyOrderQuantity;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(100001)]
    public void CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Quantity = buyOrderQuantity;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint buyOrderPrice)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Price = buyOrderPrice;

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }
    
    [Theory]
    [InlineData(10001)]
    public void CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderPrice)
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.Price = buyOrderPrice;

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.StockSymbol = null;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }
    
    [Fact]
    public void CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        buyOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31");
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateBuyOrder(buyOrderRequest);
        });
    }

    [Fact]
    public void CreateBuyOrder_ValidData_ToBeSuccessful()
    {
        // Arrange
        BuyOrderRequest buyOrderRequest = CreateValidBuyOrderRequest();
        
        // Act
        BuyOrderResponse buyOrderResponse = _stockService.CreateBuyOrder(buyOrderRequest);
        
        // Assert
        Assert.NotEqual(Guid.Empty, buyOrderResponse.BuyOrderID);
    }


    #endregion

    #region CreateSellOrder

    [Fact]
    public void CreateSellOrder_NullSellOrder_ToBeArgumentNullException()
    {
        // Arrange
        SellOrderRequest? sellOrderRequest = null;
        
        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint sellOrderQuantity)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Quantity = sellOrderQuantity;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(100001)]
    public void CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Quantity = sellOrderQuantity;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(0)]
    public void CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint sellOrderPrice)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Price = sellOrderPrice;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Theory]
    [InlineData(10001)]
    public void CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderPrice)
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.Price = sellOrderPrice;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.StockSymbol = null;
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_ValidData_ToBeSuccessful()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        sellOrderRequest.DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31");
        
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _stockService.CreateSellOrder(sellOrderRequest);
        });
    }

    [Fact]
    public void CreateSellOrder_ValidData_ToBeSuccessful()
    {
        // Arrange
        SellOrderRequest sellOrderRequest = CreateValidSellOrderRequest();
        
        // Act
        SellOrderResponse sellOrderResponse = _stockService.CreateSellOrder(sellOrderRequest);
        
        // Assert
        Assert.NotEqual(Guid.Empty, sellOrderResponse.SellOrderID);
    }
    
    #endregion

    private BuyOrderRequest CreateValidBuyOrderRequest()
    {
        return new BuyOrderRequest()
        {
            StockSymbol = "MSFT", StockName = "Microsoft",
            Price = 10, Quantity = 10
        };
    }
    
    private SellOrderRequest CreateValidSellOrderRequest()
    {
        return new SellOrderRequest()
        {
            StockSymbol = "MSFT", StockName = "Microsoft",
            Price = 10, Quantity = 10
        };
    }
}