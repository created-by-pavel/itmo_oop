using System;

namespace Banks.Models
{
    public interface IAccount
    {
        public string GetAccountNumber();
        public void TopUp(decimal money);
        public void WithDraw(decimal money);
        public void Transfer(IAccount accountTo, decimal money);
        public decimal GetBalance();
        public void TopUpPercentOrCommission(decimal money);
        public void PercentOrCommissionBalanceToZero();
        public DateTime GetTimeStart();
        public decimal GetPercentOrCommissionBalance();
    }
}