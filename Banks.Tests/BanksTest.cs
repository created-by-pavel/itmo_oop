using System;
using System.Collections.Generic;
using Banks.Models;
using Banks.Tools;
using NUnit.Framework;
namespace Banks.Tests
{
    public class Tests
    {
        private Time _time;
        private BankSystem _bs;
        private Bank _tinkov;
        private Bank _sberbank;
        private Client _pavel;
        private Client _kolya;
        private IAccount _kolyaDeposit;
        private IAccount _pavelDebit;
        private Dictionary<decimal, decimal> _depositPercents;
        private DateTime _depositTerm;
        [SetUp]
        public void Setup()
        {
            _time = new Time();
            _bs = new BankSystem(_time);
            _depositTerm = new DateTime(0001, 1, 01);
            _depositPercents = new Dictionary<decimal, decimal>
            {
                {50_000, 3},
                {100_000, 3.5m},
                {200_000, 4}
            };
            _pavel = new ClientBuilder().SetName("pavel").SetSurname("zavalnyuk").Create();
            _kolya = new ClientBuilder().SetName("kolya").SetSurname("kondratyev").SetPassportId("411777").SetAddress("dymskaya 4").Create();
            _tinkov = new Bank("tinkov", 200_000, 3.65m,-20000, 100, _depositPercents, _depositTerm);
            _sberbank = new Bank("sberbank", 1000000, 7.3m, -10000, 50, _depositPercents, _depositTerm);
            _bs.AddBank(_sberbank);
            _bs.AddBank(_tinkov);
            _kolyaDeposit = _bs.AddAccountToBank(_kolya, _sberbank, AccountType.Deposit);
            _pavelDebit = _bs.AddAccountToBank(_pavel, _tinkov, AccountType.Debit);
            _bs.TopUp(_pavel, _tinkov, _pavelDebit, 100_000);
            _bs.TopUp(_kolya, _sberbank, _kolyaDeposit, 40_000);
        }
        
        [Test]
        public void Transfer()
        {
            _bs.Transfer(_pavel, _tinkov, _pavelDebit, _kolyaDeposit, _sberbank, 1000);
            Assert.AreEqual(41000, _kolyaDeposit.GetBalance());
        }
        
        [Test]
        public void TopUp()
        {
            _bs.TopUp(_pavel, _tinkov, _pavelDebit, 1000);
            Assert.AreEqual(101_000, _pavelDebit.GetBalance());
        }
        
        [Test]
        public void WithDraw()
        {
            _bs.WithDraw(_pavel, _tinkov, _pavelDebit, 1000);
            Assert.AreEqual(99_000, _pavelDebit.GetBalance());
        }
        
        [Test]
        public void SkipDay()
        {
            _time.SkipDay();
            Assert.AreEqual(10, _pavelDebit.GetPercentOrCommissionBalance());
        }

        [Test]
        public void CancelOperation()
        {
           _bs.TopUp(_pavel, _tinkov, _pavelDebit, 10_000);
           _bs.TopUp(_pavel, _tinkov, _pavelDebit, 1);
           _bs.CancelOperation(_pavelDebit, _tinkov, _tinkov.GetCommands()[0]);
           Assert.AreEqual(10_001, _pavelDebit.GetBalance());
        }

        [Test]
        public void TimeIsNotUp_ForDeposit_ThrowException()
        {
            Assert.Catch<BanksException>(() =>
            {
                _bs.WithDraw(_kolya, _sberbank, _kolyaDeposit, 10000);
            });
        }
        
        [Test]
        public void WithDrawMoney_IsBiggerThan_TrustFactorLimit_ThrowException()
        {
            _pavelDebit.TopUp(200_000);
            Assert.Catch<BanksException>(() =>
            {
                _bs.WithDraw(_pavel, _tinkov, _pavelDebit, 201_000);
            });
        }
        
        [Test]
        public void WithDrawMoney_IsBiggerThan_CreditLimit_ThrowException()
        {
            var pavelCredit = _bs.AddAccountToBank(_pavel, _tinkov, AccountType.Credit);
            
            Assert.Catch<BanksException>(() =>
            {
                _bs.WithDraw(_pavel, _tinkov, pavelCredit, 30_000);
            });
        }
        
        
        
    }
}