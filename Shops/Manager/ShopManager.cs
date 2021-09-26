using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Tools;
namespace Shops.Manager
{
    public class ShopManager
    {
        private readonly Dictionary<Shop, ProductsList> _shops = new ();

        public Shop AddShop(string name, string address)
        {
            var newShop = new Shop(name, address);
            var productsList = new ProductsList();
            _shops.Add(newShop, productsList);
            return newShop;
        }

        public Product RegisterProduct(string name)
        {
            var newProduct = new Product(name);
            return newProduct;
        }

        public void AddProductsToShop(Product typeOfProduct, int count, Shop shop, int price)
        {
            _shops[shop].AddProduct(typeOfProduct, count);
            shop.SetPrice(typeOfProduct, price);
        }

        public void ChangePrice(Shop shop, Product typeOfProduct,  int newPrice)
        {
            shop.ChangePrice(newPrice, typeOfProduct);
        }

        public Shop FindMinimum(Product typeOfProduct, int count)
        {
            var shops = new List<Shop>();
            foreach (var shopsKVP in _shops)
            {
                if (shopsKVP.Value.ContainCount(typeOfProduct, count))
                {
                    shops.Add(shopsKVP.Key);
                }
            }

            return shops.Count > 0 ? shops.OrderBy(s => s.GetPrice(typeOfProduct)).First() : null;
        }

        public void BuyProducts(Person person, Shop shop)
        {
            decimal money = person.GetWallet();
            Dictionary<Product, int> shoppingList = person.GetShoppinglist();
            if (!_shops[shop].ContainProducts(shoppingList) || !shop.CanSell(money, shoppingList))
            {
                throw new ShopsExeption("Shop cant sell products");
            }

            _shops[shop].RemoveProducts(shoppingList);
            decimal restMoney = shop.Buy(shoppingList, money);
            person.SetMoney(restMoney);
        }
    }
}