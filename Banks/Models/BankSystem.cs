using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Banks.Tools;

namespace Banks.Models
{
    public class BankSystem
    {
        private readonly List<Bank> _banks;
        private readonly Time _time;
        public BankSystem(Time time)
        {
            _banks = new List<Bank>();
            _time = time;
            _time.MyEvent += NotifyBank;
        }

        public void AddBank(Bank bank)
        {
            _banks.Add(bank);
        }

        public List<Bank> GetBanks() => _banks.ToList();

        public IAccount AddAccountToBank(Client client, Bank bank, AccountType accountType)
        {
           // if (client.GetName() == null || client.GetSurname() == null) throw new BanksException("write your name and surname");
            return bank.AddAccountToBank(client, accountType, _time);
        }

        public void Transfer(Client client, Bank bankFrom, IAccount accountFrom, IAccount accountTo, Bank bankTo, decimal money)
        {
            if (!_banks.Contains(bankFrom) || !_banks.Contains(bankTo)) throw new BanksException("cant find bank");
            if (!bankFrom.GetClientAccounts().ContainsKey(client)) throw new BanksException("cant find client");
            if (!bankFrom.GetClientAccounts()[client].Contains(accountFrom)) throw new BanksException("cant find account");
            if (client.TrustFactor() == false && money > bankFrom.GetTrustFactorLimit()) throw new BanksException("sum > trustFactorLimit");
            if (!bankTo.GetClientAccounts().Values.Any(list => list.Contains(accountTo))) throw new BanksException("cant find accountTo");
            bankFrom.SetCommand(new TransferCommand(accountFrom, accountTo, money));
            bankFrom.RunCommand();
        }

        public void TopUp(Client client, Bank bank, IAccount account, decimal money)
        {
            if (!_banks.Contains(bank)) throw new BanksException("cant find bank");
            if (!bank.GetClientAccounts().ContainsKey(client)) throw new BanksException("cant client");
            if (!bank.GetClientAccounts()[client].Contains(account)) throw new BanksException("cant find account");
            bank.SetCommand(new TopUpCommand(account, money));
            bank.RunCommand();
        }

        public void WithDraw(Client client, Bank bank, IAccount account, decimal money)
        {
            if (!_banks.Contains(bank)) throw new BanksException("cant find bank");
            if (!bank.GetClientAccounts().ContainsKey(client)) throw new BanksException("cant find client");
            if (!bank.GetClientAccounts()[client].Contains(account)) throw new BanksException("cant find account");
            if (client.TrustFactor() == false && money > bank.GetTrustFactorLimit()) throw new BanksException("sum > trustFactorLimit");
            bank.SetCommand(new WithDrawCommand(account, money));
            bank.RunCommand();
        }

        public void CancelOperation(IAccount account, Bank bank, ICommand command)
        {
            if (!_banks.Contains(bank)) throw new BanksException("cant find bank");
            if (!bank.GetClientAccounts().Values.Any(list => list.Contains(account))) throw new BanksException("cant find account");
            bank.CancelCommand(command);
        }

        private void NotifyBank()
        {
            foreach (var bank in _banks)
            {
                foreach (var account in bank.GetClientAccounts().SelectMany(kvp => kvp.Value))
                {
                    if (_time.GetTime().Month - account.GetTimeStart().Month >= 1 &&
                        _time.GetTime().Day - account.GetTimeStart().Day == 0)
                    {
                        bank.Notify(account);
                    }
                    else
                    {
                        bank.TopUpPercents(account);
                    }
                }
            }
        }
    }
}