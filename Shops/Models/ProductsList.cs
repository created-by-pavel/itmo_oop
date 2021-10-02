using System.Collections.Generic;
namespace Shops.Models
{
    public class ProductsList
    {
        private readonly Dictionary<Product, int> _productsList = new ();

        public void AddProduct(Product typeOfProduct, int count)
        {
            if (_productsList.ContainsKey(typeOfProduct))
            {
                _productsList[typeOfProduct] += count;
                return;
            }

            _productsList.Add(typeOfProduct, count);
        }

        public bool ContainCount(Product typeOfProduct, int count)
        {
            if (_productsList.ContainsKey(typeOfProduct))
            {
                return _productsList[typeOfProduct] >= count;
            }

            return false;
        }

        public bool ContainProducts(Dictionary<Product, int> shoppingList)
        {
            bool check = true;
            foreach (var shoppingKVP in shoppingList)
            {
                if (!_productsList.ContainsKey(shoppingKVP.Key) || _productsList[shoppingKVP.Key] < shoppingKVP.Value)
                {
                    check = false;
                }
            }

            return check;
        }

        public void RemoveProducts(Dictionary<Product, int> shoppingList)
        {
            foreach (var shoppingKVP in shoppingList)
            {
                _productsList[shoppingKVP.Key] -= shoppingKVP.Value;
            }
        }
    }
}