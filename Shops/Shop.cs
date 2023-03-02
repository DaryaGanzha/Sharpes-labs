using System;
using System.Collections.Generic;
using System.Net.Security;

namespace Shops
{
    public class Shop
    {
        private string _shopName;
        private Guid _shopId;
        private string _shopAddress;
        private List<Delivery> _deliveryList = new List<Delivery>();

        public Shop(string name, string address)
        {
            _shopName = name;
            _shopAddress = address;
            _shopId = Guid.NewGuid();
        }

        public List<Delivery> GetDeliveryList() => _deliveryList;

        public void AddProduct(Delivery supply)
        {
            int foundProduct = -1;
            if (FindDeliveryNumberInList(this.FindDelivery(supply.GetProductName())) != -1)
            {
                foundProduct = FindDeliveryNumberInList(this.FindDelivery(supply.GetProductName()));
            }

            if (foundProduct != -1)
            {
                var newDelivery = new Delivery(
                    _deliveryList[foundProduct].Product,
                    _deliveryList[foundProduct].Cost,
                    _deliveryList[foundProduct].Count + supply.Count);
                _deliveryList[foundProduct] = newDelivery;
            }
            else
            {
                var newDelivery = new Delivery(
                    supply.Product,
                    supply.Cost,
                    supply.Count);
                _deliveryList.Add(newDelivery);
            }
        }

        public void ChangeProductCost(Product product, int newCost)
        {
            int foundProduct = -1;
            for (int i = 0; i < _deliveryList.Count; i++)
            {
                if (_deliveryList[i].Product == product)
                {
                    foundProduct = i;
                    break;
                }
            }

            if (foundProduct != -1)
            {
                var newDelivery = new Delivery(
                    _deliveryList[foundProduct].Product,
                    newCost,
                    _deliveryList[foundProduct].Count);
                _deliveryList[foundProduct] = newDelivery;
            }
        }

        public Client BuyProductList(List<SaleItem> shoppingList, Client client)
        {
            int count = 0;
            foreach (SaleItem item in shoppingList)
            {
                int costProduct = 0;

                if (_deliveryList.Find(delivery => delivery.Product == item.Product) != null)
                {
                    costProduct = _deliveryList.Find(delivery => delivery.Product == item.Product)
                     .Cost;
                }
                else
                {
                    throw new ShopException($"There is no product {item.GetProductName()} in the store {_shopName}.");
                }

                count += item.QuantityOfGoods * costProduct;
            }

            if (count > client.Money)
            {
                throw new ShopException("The buyer does not have enough money.");
            }

            foreach (SaleItem item in shoppingList)
            {
                Delivery ourDelivery = FindDelivery(item.GetProductName());
                if (ourDelivery != null)
                {
                    if (item.QuantityOfGoods <= ourDelivery.Count)
                    {
                        var newDelivery = new Delivery(
                            ourDelivery.Product,
                            ourDelivery.Cost,
                            ourDelivery.Count - item.QuantityOfGoods);
                        _deliveryList[FindDeliveryNumberInList(ourDelivery)] = newDelivery;
                    }
                    else
                    {
                        throw new Exception(
                            $"There is not enough product {_deliveryList[FindDeliveryNumberInList(ourDelivery)].GetProductName()} in store.");
                    }
                }
            }

            int newMoney = client.Money - count;
            var newClient = new Client(client.ClientName, newMoney);
            return newClient;
        }

        public Delivery FindDelivery(string productName)
        {
            return _deliveryList.Find(item => item.GetProductName() == productName);
        }

        private int FindDeliveryNumberInList(Delivery supply)
        {
            int deliveryNumber = -1;
            int k = 0;
            for (int i = 0; i < _deliveryList.Count; i++)
            {
                if (_deliveryList[i] == supply)
                {
                    deliveryNumber = i;
                    k += 1;
                    break;
                }
            }

            if (k == 1)
            {
                return deliveryNumber;
            }
            else
            {
                return -1;
            }
        }
    }
}