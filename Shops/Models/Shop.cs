using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        private readonly Dictionary<Product, int> _productPrice = new ();
        private string _shopId;
        private string _shopName;
        private string _address;
        private decimal _cash;
        public Shop(string shopName, string address)
        {
            _shopName = shopName;
            _address = address;
            _shopId = Guid.NewGuid().ToString();
            _cash = 0;
        }

        public void SetPrice(Product typeOfProduct, int price)
        {
            if (_productPrice.ContainsKey(typeOfProduct))
            {
                return;
            }

            _productPrice.Add(typeOfProduct, price);
        }

        public int GetPrice(Product typeOfProduct)
        {
            return _productPrice[typeOfProduct];
        }

        public void ChangePrice(int newPrice, Product typeOfProduct)
        {
            if (!_productPrice.ContainsKey(typeOfProduct)) throw new ShopsExeption("Product doesnt exist");
            _productPrice[typeOfProduct] = newPrice;
        }

        public bool CanSell(decimal money, Dictionary<Product, int> shoppingList)
        {
            int totalPrice = shoppingList.Keys.Sum(product => _productPrice[product] * shoppingList[product]);
            return money >= totalPrice;
        }

        public decimal Buy(Dictionary<Product, int> shoppingList, decimal money)
        {
            foreach (var shoppingKVP in shoppingList)
            {
                _cash += _productPrice[shoppingKVP.Key] * shoppingKVP.Value;
                money -= _productPrice[shoppingKVP.Key] * shoppingKVP.Value;
            }

            return money;
        }
    }
}