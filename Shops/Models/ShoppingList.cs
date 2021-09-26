using System.Collections.Generic;
namespace Shops.Models
{
    public class ShoppingList
    {
        private readonly Dictionary<Product, int> _shoppingList = new ();

        public Dictionary<Product, int> GetShoppingList() => _shoppingList;
        public void AddItems(Product typeOfProduct, int count)
        {
            _shoppingList.Add(typeOfProduct, count);
        }
    }
}