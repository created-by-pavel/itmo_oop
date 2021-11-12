using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banks.Tools;

namespace Banks.Models
{
    public class Bank : IEquatable<Bank>
    {
        private readonly Dictionary<Client, List<IAccount>> _clientAccounts;
        private readonly List<ICommand> _commands;
        private readonly decimal _commission;
        private readonly DateTime _depositTerm;
        private readonly string _name;
        private decimal _trustFactorLimit;
        private decimal _debitPercent;
        private decimal _creditLimit;
        private Dictionary<decimal, decimal> _depositPercents;                      // <money, percent>
        private ICommand _command;

        public Bank(
            string name,
            decimal trustFactorLimit,
            decimal debitPercent,
            decimal creditLimit,
            decimal creditCommission,
            Dictionary<decimal, decimal> depositPercents,
            DateTime depositTerm)
        {
            _name = name;
            _trustFactorLimit = trustFactorLimit;
            _debitPercent = debitPercent / 365;
            _creditLimit = creditLimit;
            _commission = creditCommission;
            _depositPercents = depositPercents;
            _clientAccounts = new Dictionary<Client, List<IAccount>>();
            _depositTerm = depositTerm;
            _commands = new List<ICommand>();
        }

        public Bank() { }

        public string GetBankName() => _name;

        public IAccount AddAccountToBank(Client client, AccountType accountType, Time time)
        {
            short accountNumCount = 16;
            var random = new Random();
            var accountNum = new StringBuilder();
            while (accountNum.Length < accountNumCount)
            {
                accountNum.Append(random.Next(10));
            }

            if (!_clientAccounts.ContainsKey(client))
            {
                _clientAccounts.Add(client, new List<IAccount>());
            }

            // AccountFactory
            switch (accountType)
            {
                case AccountType.Credit:
                    Credit newCredit = new (accountNum.ToString(), _creditLimit, _commission);
                    _clientAccounts[client].Add(newCredit);
                    return newCredit;
                case AccountType.Debit:
                    Debit newDebit = new (accountNum.ToString());
                    _clientAccounts[client].Add(newDebit);
                    return newDebit;
                case AccountType.Deposit:
                    Deposit newDeposit = new (accountNum.ToString(), _depositTerm, time);
                    _clientAccounts[client].Add(newDeposit);
                    return newDeposit;
                default:
                    throw new BanksException("cant define this account");
            }
        }

        public void SetCommand(ICommand command)
        {
            _command = command;
            _commands.Add(command);
        }

        public void RunCommand()
        {
            _command.Execute();
        }

        public void CancelCommand(ICommand command)
        {
            if (!_commands.Contains(command)) throw new BanksException("bank cant fount this operation");
            command.Undo();
        }

        public void Notify(IAccount account)
        {
            switch (account)
            {
                case Debit or Deposit:
                    account.TopUp(account.GetPercentOrCommissionBalance());
                    account.PercentOrCommissionBalanceToZero();
                    return;
                case Credit:
                    account.TopUp(-account.GetPercentOrCommissionBalance());
                    account.PercentOrCommissionBalanceToZero();
                    return;
            }
        }

        public void TopUpPercents(IAccount account)
        {
            switch (account)
            {
                case Debit:
                    account.TopUpPercentOrCommission(account.GetBalance() * _debitPercent / 100);
                    return;
                case Deposit:
                    account.TopUpPercentOrCommission(account.GetBalance() * SelectDepositPercent(account) / 100);
                    return;
            }
        }

        public void Attach(Client client)
        {
            client.Subscription();
        }

        public void ChangeDebitPercent(decimal debitPercent)
        {
            _debitPercent = debitPercent;
            NotifyDebitClient();
        }

        public void ChangeCreditLimit(decimal creditLimit)
        {
            _creditLimit = creditLimit;
            NotifyCreditClient();
        }

        public void ChangeTrustFactorLimit(decimal newTrustFactorLimit)
        {
            _trustFactorLimit = newTrustFactorLimit;
            NotifyBadTrustFactorClient();
        }

        public void ChangeDepositPercents(Dictionary<decimal, decimal> depositPercents)
        {
            _depositPercents = depositPercents;
            NotifyDepositClient();
        }

        public List<ICommand> GetCommands() => _commands.ToList();

        public decimal GetTrustFactorLimit() => _trustFactorLimit;
        public bool Equals(Bank other)
        {
            if (other == null)
                return false;
            return _name == other._name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Bank productObj)
                return false;
            else
                return Equals(productObj);
        }

        public override int GetHashCode() => _name.GetHashCode() ^ _clientAccounts.GetHashCode();

        public Dictionary<Client, List<IAccount>> GetClientAccounts() => _clientAccounts;

        private decimal SelectDepositPercent(IAccount account)
        {
            foreach (var kvp in _depositPercents)
            {
                if (account.GetBalance() > kvp.Key) continue;
                return kvp.Value / 365;
            }

            return _depositPercents.Values.Last() / 365;
        }

        private void NotifyCreditClient()
        {
            foreach (var kvp in _clientAccounts)
            {
                if (kvp.Key.GetSubscription() == true && kvp.Value.FirstOrDefault(a => a is Credit) != null)
                {
                    kvp.Key.CreditLimitUpdated();
                }
            }
        }

        private void NotifyDebitClient()
        {
            foreach (var kvp in _clientAccounts)
            {
                if (kvp.Key.GetSubscription() == true && kvp.Value.FirstOrDefault(a => a is Debit) != null)
                {
                    kvp.Key.DebitPercentsUpdated();
                }
            }
        }

        private void NotifyDepositClient()
        {
            foreach (var kvp in _clientAccounts)
            {
                if (kvp.Key.GetSubscription() == true && kvp.Value.FirstOrDefault(a => a is Deposit) != null)
                {
                    kvp.Key.DepositPercentsUpdated();
                }
            }
        }

        private void NotifyBadTrustFactorClient()
        {
            foreach (var kvp in _clientAccounts)
            {
                if (kvp.Key.GetSubscription() == true && kvp.Key.TrustFactor() == false)
                {
                    kvp.Key.TrustFactorLimitUpdated();
                }
            }
        }
    }
}