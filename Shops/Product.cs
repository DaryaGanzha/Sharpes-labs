using System;
using System.IO;

namespace Shops
{
    public class Product : IEquatable<Product>
    {
        public Product(string name)
        {
            ProductName = name;
        }

        public string ProductName { get; }

        public bool Equals(Product other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductName == other.ProductName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product)obj);
        }

        public override int GetHashCode()
        {
            return ProductName != null ? ProductName.GetHashCode() : 0;
        }
    }
}