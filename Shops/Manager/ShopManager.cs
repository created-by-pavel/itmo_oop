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
            if (typeOfProduct == null || count <= 0 || shop == null || price <= 0)
            {
                throw new ShopsExeption("bad arguments");
            }

            _shops[shop].AddProduct(typeOfProduct, count);
            shop.SetPrice(typeOfProduct, price);
        }

        public void ChangePrice(Shop shop, Product typeOfProduct,  int newPrice)
        {
            if (shop == null || typeOfProduct == null || newPrice <= 0)
            {
                throw new ShopsExeption("bad arguments");
            }

            shop.ChangePrice(newPrice, typeOfProduct);
        }

        public Shop FindMinimum(Product typeOfProduct, int count)
        {
            if (typeOfProduct == null || count <= 0)
            {
                throw new ShopsExeption("bad arguments");
            }

            var shopsList = new List<Shop>();
            foreach (var shopsKVP in _shops)
            {
                if (shopsKVP.Value.ContainCount(typeOfProduct, count))
                {
                    shopsList.Add(shopsKVP.Key);
                }
            }

            return shopsList.Count > 0 ? shopsList.OrderBy(s => s.GetPrice(typeOfProduct)).First() : null;
        }

        public void BuyProducts(Person person, Shop shop)
        {
            if (person == null || shop == null)
            {
                throw new ShopsExeption("bad arguments");
            }

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