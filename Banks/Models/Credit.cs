using System;
using System.ComponentModel.Design;
using Banks.Tools;

namespace Banks.Models
{
    public class Credit : IAccount
    {
        private readonly DateTime _timeStart;
        private readonly decimal _creditLimit;
        private decimal _commission;
        private decimal _balance;
        private decimal _commissionBalance;
        private string _accountNum;
        private string _id;

        public Credit(string accountNum, decimal creditLimit, decimal commission)
        {
            _timeStart = DateTime.Today;
            _accountNum = accountNum;
            _creditLimit = creditLimit;
            _commission = commission;
            _id = Guid.NewGuid().ToString();
        }

        public string GetAccountNumber() => _accountNum;

        public void TopUp(decimal money)
        {
            _balance += money;
        }

        public void Transfer(IAccount accountTo, decimal money)
        {
            if (_balance < 0)
            {
                if (_balance - money - _commission <= _creditLimit) throw new BanksException("insufficient funds");
                _balance -= money;
                accountTo.TopUp(money);
                TopUpPercentOrCommission(_commission);
                return;
            }

            _balance -= money;
            accountTo.TopUp(money);
        }

        public void WithDraw(decimal money)
        {
            if (_balance <= 0)
            {
                if (_balance - money - _commission <= _creditLimit) throw new BanksException("insufficient funds");
                _balance -= money;
                TopUpPercentOrCommission(_commission);
                return;
            }

            _balance -= money;
        }

        public void TopUpPercentOrCommission(decimal money)
        {
            _commissionBalance += money;
        }

        public void PercentOrCommissionBalanceToZero()
        {
            _commission = 0;
        }

        public decimal GetBalance() => _balance;
        public decimal GetPercentOrCommissionBalance() => _commissionBalance;
        public DateTime GetTimeStart() => _timeStart;
        public void MinusSum(decimal money) { _balance -= money; }
    }
}