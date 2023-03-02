using System;

namespace Shops
{
    public class Delivery : IEquatable<Delivery>
    {
        public Delivery(Product product, int cost, int count)
        {
            Product = product;
            Cost = cost;
            Count = count;
        }

        public Product Product { get; }
        public int Cost { get; }
        public int Count { get; }
        public string GetProductName() => Product.ProductName;

        public bool Equals(Delivery other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Product, other.Product) && Cost == other.Cost && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Delivery)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Product, Cost, Count);
        }
    }
}