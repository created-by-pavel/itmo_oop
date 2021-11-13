using System;
using Banks.Tools;

namespace Banks.Models
{
    public class Debit : IAccount
    {
        private readonly DateTime _timeStart;
        private decimal _balance;
        private string _accountNum;
        private string _id;
        private decimal _percentBalance;

        public Debit(string accountNum)
        {
            _timeStart = DateTime.Today;
            _accountNum = accountNum;
            _id = Guid.NewGuid().ToString();
        }

        public string GetAccountNumber() => _accountNum;

        public void Transfer(IAccount accountTo, decimal money)
        {
            if (_balance < money) throw new BanksException("insufficient funds");
            _balance -= money;
            accountTo.TopUp(money);
        }

        public void TopUp(decimal money) { _balance += money; }

        public void WithDraw(decimal money)
        {
            if (_balance < money) throw new BanksException("insufficient funds");
            _balance -= money;
        }

        public void TopUpPercentOrCommission(decimal money)
        {
            _percentBalance += money;
        }

        public void PercentOrCommissionBalanceToZero()
        {
            _percentBalance = 0;
        }

        public decimal GetPercentOrCommissionBalance() => _percentBalance;

        public decimal GetBalance() => _balance;
        public DateTime GetTimeStart() => _timeStart;
        public void MinusSum(decimal money) { _balance -= money; }
    }
}