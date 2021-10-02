using System.Collections.Generic;

namespace Shops.Models
{
    public class Person
    {
        private readonly ShoppingList _shoppingList;
        private decimal _wallet;

        public Person(int money)
        {
            _wallet = money;
            _shoppingList = new ShoppingList();
        }

        public void AddToShopping(Product typeOfProduct, int count)
        {
            _shoppingList.AddItems(typeOfProduct, count);
        }

        public decimal GetWallet() => _wallet;
        public Dictionary<Product, int> GetShoppinglist() => _shoppingList.GetShoppingList();

        public void SetMoney(decimal money)
        {
            _wallet = money;
        }
    }
}