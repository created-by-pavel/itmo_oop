using System;
using Microsoft.Win32.SafeHandles;
using NUnit.Framework;
using Shops.Models;
using Shops.Manager;
using Shops.Tools;
namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        private Shop _shop1;
        private Shop _shop2;
        private Shop _shop3;
        private Product _marijuana;
        private Product _xanax;
        private Product _heroin;
        private Product _crack;
        private Product _cocaine;
        private Product _mephedrone;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
            _shop1 = _shopManager.AddShop("y Kizaru", "Dymskaya 3");
            _shop2 = _shopManager.AddShop("y Pashi Technica", "Dymskaya 4");
            _shop3 = _shopManager.AddShop("y ASAP Rocky", "Dymskaya 5");
            _marijuana = _shopManager.RegisterProduct("marijuana");
            _xanax =_shopManager.RegisterProduct("xanax");
            _heroin =_shopManager.RegisterProduct("heroin");
            _crack =_shopManager.RegisterProduct("crack");
            _cocaine =_shopManager.RegisterProduct("cocaine");
            _mephedrone =_shopManager.RegisterProduct("mephedrone");
            _shopManager.AddProductsToShop(_marijuana, 10, _shop1, 100);
            _shopManager.AddProductsToShop(_marijuana, 10, _shop1, 100);
            _shopManager.AddProductsToShop(_xanax, 10, _shop2, 200);
            _shopManager.AddProductsToShop(_mephedrone, 10, _shop2, 100);
            _shopManager.AddProductsToShop(_crack, 10, _shop3, 250);
            _shopManager.AddProductsToShop(_cocaine, 10, _shop3, 90);
            _shopManager.AddProductsToShop(_heroin, 10, _shop1, 300);
            _shopManager.AddProductsToShop(_xanax, 5, _shop3,150);
        }

        [Test]
        public void ChangePrice()
        {
            _shopManager.ChangePrice(_shop1, _marijuana, 110);
            Assert.AreEqual( 110, _shop1.GetPrice(_marijuana));
        }
        
        [Test]
        public void ChangePrice_ShopDoesntExistProduct_ThrowException()
        {
            Assert.Catch<ShopsExeption>(() =>
            {
                _shopManager.ChangePrice(_shop1, _cocaine, 110);
            });
        }

        [Test]
        public void FindMinimum()
        {
            Assert.AreEqual(_shop3, _shopManager.FindMinimum(_xanax, 4));
        }

        [Test]
        public void MinimumCantFind_ReturnNull()
        {
            Assert.AreEqual(null, _shopManager.FindMinimum(_xanax, 20));
        }

        [Test]
        public void BuyProducts()
        {
            var user = new Person(10000);
            user.AddToShopping(_heroin, 2);
            user.AddToShopping(_marijuana, 3);
            _shopManager.BuyProducts(user, _shop1);
            Assert.AreEqual(9100, user.GetWallet());
        }

        [Test]
        public void BuyProducts_PersonHasNoMoney_ThrowException()
        {
            Assert.Catch<ShopsExeption>(() =>
            {
                var user = new Person(10);
                user.AddToShopping(_heroin, 2);
                user.AddToShopping(_marijuana, 3);
                _shopManager.BuyProducts(user, _shop1);
            });
        }
    }
}