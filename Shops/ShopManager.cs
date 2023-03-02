using System.Collections.Generic;

namespace Shops
{
    public class ShopManager
    {
        private List<Shop> _shopList;

        public ShopManager()
        {
            _shopList = new List<Shop>();
        }

        public Shop AddShop(string name, string address)
        {
            var newShop = new Shop(name, address);
            _shopList.Add(newShop);
            return newShop;
        }

        public Shop CheapShop(Product product)
        {
            Shop cheapShop = null;
            int minPrice = int.MaxValue;
            foreach (Shop shop in _shopList)
            {
                Delivery newDelivery = shop.FindDelivery(product.ProductName);
                if (newDelivery != null)
                {
                    if (newDelivery.Product == product)
                    {
                        if (minPrice > newDelivery.Cost)
                        {
                            minPrice = newDelivery.Cost;
                            cheapShop = shop;
                        }
                    }
                }
            }

            return cheapShop;
        }
    }
}