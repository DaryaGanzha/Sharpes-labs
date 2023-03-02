namespace Shops
{
    public class SaleItem
    {
        public SaleItem(Product product, int quantityOfGoods)
        {
            Product = product;
            QuantityOfGoods = quantityOfGoods;
        }

        public int QuantityOfGoods { get; }
        public Product Product { get; }
        public string GetProductName() => Product.ProductName;
    }
}