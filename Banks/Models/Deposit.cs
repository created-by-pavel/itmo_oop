using System;
using Banks.Tools;

namespace Banks.Models
{
    public class Deposit : IAccount
    {
        private readonly DateTime _timeStart;
        private readonly DateTime _depositTerm;
        private readonly Time _time;
        private decimal _balance;
        private decimal _percentBalance;
        private string _accountNum;
        private string _id;
        public Deposit(string accountNum, DateTime depositTerm, Time time)
        {
            _timeStart = DateTime.Today;
            _accountNum = accountNum;
            _depositTerm = _timeStart.AddDays(depositTerm.Day).AddMonths(depositTerm.Month).AddYears(depositTerm.Year);
            _time = time;
            _id = Guid.NewGuid().ToString();
        }

        public string GetAccountNumber() => _accountNum;

        public void TopUp(decimal money) { _balance += money; }

        public void Transfer(IAccount accountTo, decimal money)
        {
            if (_time.GetTime().CompareTo(_depositTerm) < 0) throw new BanksException("you can't do it yet");
            if (_balance < money) throw new BanksException("insufficient funds");
            _balance -= money;
            accountTo.TopUp(money);
        }

        public void WithDraw(decimal money)
        {
            if (_time.GetTime().CompareTo(_depositTerm) < 0) throw new BanksException("you can't do it yet");
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

        public decimal GetBalance() => _balance;
        public decimal GetPercentOrCommissionBalance() => _percentBalance;
        public DateTime GetTimeStart() => _timeStart;
        public void MinusSum(decimal money) { _balance -= money; }
    }
}