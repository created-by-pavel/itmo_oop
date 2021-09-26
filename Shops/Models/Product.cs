using System;

namespace Shops.Models
{
    public class Product : IEquatable<Product>
    {
        private readonly string _productId;
        private readonly string _productName;

        public Product(string productName)
        {
            _productName = productName;
            _productId = Guid.NewGuid().ToString();
        }

        public bool Equals(Product other)
        {
            if (other == null)
                return false;
            return _productId == other._productId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Product productObj)
                return false;
            else
                return Equals(productObj);
        }

        public override int GetHashCode() => _productId.GetHashCode() ^ _productName.GetHashCode();
    }
}