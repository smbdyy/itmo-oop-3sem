using Shops.Entities;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopsTest
{
    [Fact]
    public void CreateShopAndSupplyProducts_ProductsAreAvailableToBuy()
    {
        var shopManager = new ShopManager();
        Shop shop = shopManager.RegisterShop("My shop", "221B Baker street");
        Product product = shopManager.RegisterProduct("stuff");
        shop.Supply(new List<ShopItem> { new (product, 3, 10) });

        Assert.True(shop.IsOrderAvailableToBuy(new List<OrderItem> { new (product, 3) }));
    }

    [Theory]
    [InlineData(20)]
    [InlineData(30)]
    public void ChangeShopItemPrice_PriceChanged(decimal newPrice)
    {
        var shopManager = new ShopManager();
        Shop shop = shopManager.RegisterShop("My shop", "221B Baker street");
        Product product = shopManager.RegisterProduct("stuff");
        shop.Supply(new List<ShopItem> { new (product, 3, 10) });

        shop.ChangeProductPrice(product, newPrice);
        Assert.Equal(newPrice, shop.GetProductInfo(product).Price);
    }

    [Fact]
    public void FindShopWithLowestCost_FoundShopHasTheLowestOrderCost()
    {
        var shopManager = new ShopManager();
        Product product = shopManager.RegisterProduct("stuff");

        Shop cheapestShop = shopManager.RegisterShop("Cheapest shop", "Kronverksky, 9");
        cheapestShop.Supply(new List<ShopItem> { new (product, 3, 10) });

        Shop expensiveShop = shopManager.RegisterShop("Expensive shop", "Lomonosova, 49");
        cheapestShop.Supply(new List<ShopItem> { new (product, 3, 15) });

        Shop? foundShop = shopManager.FindShopWithLowestCost(new List<OrderItem> { new (product, 2) });
        Assert.Equal(cheapestShop, foundShop);
    }

    [Fact]
    public void FindShopWithLowestCostWhenOrderingUnavailableProduct_FoundShopIsNull()
    {
        var shopManager = new ShopManager();
        Shop shop = shopManager.RegisterShop("My shop", "221B Baker street");
        Product availableProduct = shopManager.RegisterProduct("stuff");
        shop.Supply(new List<ShopItem> { new (availableProduct, 3, 10) });

        Product unavailableProduct = shopManager.RegisterProduct("unavailable stuff");
        Shop? foundShop = shopManager.FindShopWithLowestCost(new List<OrderItem> { new (unavailableProduct, 3) });
        Assert.Null(foundShop);
    }

    [Fact]
    public void BuyProducts_ProductAndMoneyAmountChanged()
    {
        var shopManager = new ShopManager();
        Shop shop = shopManager.RegisterShop("My Shop", "221B Baker street");

        Product product1 = shopManager.RegisterProduct("stuff number one");
        Product product2 = shopManager.RegisterProduct("stuff number two");
        const int product1AmountBefore = 10;
        const int product2AmountBefore = 10;

        shop.Supply(new List<ShopItem> { new (product1, product1AmountBefore, 10) });
        shop.Supply(new List<ShopItem> { new (product2, product2AmountBefore, 15) });

        const int product1AmountToBuy = 3;
        const int product2AmountToBuy = 4;
        var order = new List<OrderItem>
        {
            new (product1, product1AmountToBuy),
            new (product2, product2AmountToBuy),
        };

        const decimal moneyBefore = 1000;
        decimal orderCost = shop.CountOrderCost(order);
        var person = new Person("John Doe", moneyBefore);
        shop.Buy(person, order);

        Assert.Equal(moneyBefore - orderCost, person.Money);
        Assert.Equal(product1AmountBefore - product1AmountToBuy, shop.GetProductInfo(product1).Amount);
        Assert.Equal(product2AmountBefore - product2AmountToBuy, shop.GetProductInfo(product2).Amount);
    }
}