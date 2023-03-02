using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Shops.Tests
{
    public class ShopTest
    {

        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void DeliveryProductsIntoShop_ProductsAreInShop()
        {
            var shop1 = new Shop("shop1", "address1");
            var product1 = new Product("product1");
            var product2 = new Product("product2");
            var product3 = new Product("product3");
            var delivery1 = new Delivery(product1, 100, 100);
            var delivery2 = new Delivery(product2, 1000, 4);
            var delivery3 = new Delivery(product3, 45, 6);
            var delivery4 = new Delivery(product3, 50, 4);
            shop1.AddProduct(delivery1);
            shop1.AddProduct(delivery2);
            shop1.AddProduct(delivery3);
            shop1.AddProduct(delivery4);
            Assert.AreEqual(10, shop1.FindDelivery(product3.ProductName).Count);
            Assert.AreEqual(45, shop1.FindDelivery(product3.ProductName).Cost);
        }

        [Test]
        public void ChangeProductCost_CostChanged()
        {
            Shop shop1 = _shopManager.AddShop("shop1", "address1");
            var product1 = new Product("product1");
            var delivery1 = new Delivery(product1, 100, 100);
            shop1.AddProduct(delivery1);
            Assert.AreEqual(100, shop1.FindDelivery(product1.ProductName).Cost);
            shop1.ChangeProductCost(product1, 150);
            Assert.AreEqual(150, shop1.FindDelivery(product1.ProductName).Cost);
        }

        [Test]
        public void FindShopWithCheapestCost_FindShop()
        {
            Shop shop1 = _shopManager.AddShop("shop1", "address1");
            Shop shop2 = _shopManager.AddShop("shop2", "address2");
            Shop shop3 = _shopManager.AddShop("shop3", "address3");
            var product1 = new Product("product1");
            var product2 = new Product("product2");
            var product3 = new Product("product3");
            var supply1 = new Delivery(product1, 100, 100);
            var supply2 = new Delivery(product1, 10, 120);
            var supply3 = new Delivery(product1, 50, 90);
            var supply4 = new Delivery(product2, 7, 1000);
            var supply5 = new Delivery(product2, 6, 45);
            var supply6 = new Delivery(product3, 4, 50);
            var supply7 = new Delivery(product3, 5, 70);
            shop1.AddProduct(supply1);
            shop1.AddProduct(supply4);
            shop1.AddProduct(supply6);
            shop2.AddProduct(supply2);
            shop2.AddProduct(supply5);
            shop2.AddProduct(supply7);
            shop3.AddProduct(supply3);
            Assert.AreEqual(shop2, _shopManager.CheapShop(product1));
            Assert.AreEqual(shop2, _shopManager.CheapShop(product2));
            Assert.AreEqual(shop1, _shopManager.CheapShop(product3));
        }

        [Test]
        public void ClientBuyProducts_ClientMoneyChanged()
        {
            Shop shop1 = _shopManager.AddShop("shop1", "address1");
            var product1 = new Product("product1");
            var product2 = new Product("product2");
            var product3 = new Product("product3");
            var delivery1 = new Delivery(product1, 100, 100);
            var delivery2 = new Delivery(product2, 4, 1000);
            var delivery3 = new Delivery(product3, 6, 45);
            shop1.AddProduct(delivery1);
            shop1.AddProduct(delivery2);
            shop1.AddProduct(delivery3);
            Console.WriteLine(shop1.GetDeliveryList().Count);

            var listProduct = new List<SaleItem>();
            var productToBuy1 = new SaleItem(product1, 11);
            var productToBuy2 = new SaleItem(product2, 1);
            var productToBuy3 = new SaleItem(product3, 1);
            listProduct.Add(productToBuy1);
            listProduct.Add(productToBuy2);
            listProduct.Add(productToBuy3);

            var client = new Client("customer1", 3000);
            client = shop1.BuyProductList(listProduct, client);
        }
    }
}