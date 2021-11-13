using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using Banks.Models;

namespace Banks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var time = new Time();
            var bs = new BankSystem(time);
            string name, surname, address, passportId, bankName, choice, accountNumber;
            decimal trustFactorLimit, debitPercent, creditLimit, creditCommission, sum, percent, money;
            int years, months, days, accountType, count;
            IAccount account = null;
            Client client = new Client();
            var bank = new Bank();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("COMMANDS TO CREATE");
            Console.ResetColor();
            Console.WriteLine("click '1' to create client");
            Console.WriteLine("click '2' to create Bank");
            Console.WriteLine("click '3' to create add Account to bank");
            Console.WriteLine("click '4' to make Operation");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("COMMANDS TO SKIP TIME");
            Console.ResetColor();
            Console.WriteLine("click '5' to skip day");
            Console.WriteLine("click '6' to skip month");
            Console.WriteLine("click '7' to skip year");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("COMMANDS TO EDIT");
            Console.ResetColor();
            Console.WriteLine("click '8' to add passportId or address to client");
            Console.WriteLine("click '9' to edit Percents or commission");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("COMMANDS TO GET");
            Console.ResetColor();
            Console.WriteLine("click '0' to get Balance");
            Console.WriteLine("enter 'exit' to stop program");
            var clients = new List<Client>();
            while (true)
            {
                var a = Console.ReadLine();
                switch (a)
                {
                    case "exit":
                        return;
                    case "0":
                        Console.WriteLine("enter account number");
                        accountNumber = Console.ReadLine();
                        foreach (var b in bs.GetBanks())
                        {
                            foreach (var kvp in b.GetClientAccounts())
                            {
                                foreach (var ac in kvp.Value)
                                {
                                    if (ac.GetAccountNumber() == accountNumber)
                                    {
                                        Console.WriteLine(ac.GetBalance());
                                    }
                                }
                            }
                        }

                        break;
                    case "1":
                        Console.WriteLine("you must enter name and surname");
                        Console.WriteLine("enter name");
                        name = Console.ReadLine();

                        Console.WriteLine("enter surname");
                        surname = Console.ReadLine();

                        clients.Add(new ClientBuilder().SetName(name).SetSurname(surname).Create());
                        break;

                    case "2":
                        Console.WriteLine("enter bank name");
                        bankName = Console.ReadLine();

                        Console.WriteLine("enter trustFactorLimit");
                        trustFactorLimit = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("enter debitPercent");
                        debitPercent = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("enter creditLimit (negative number)");
                        creditLimit = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("enter creditCommission");
                        creditCommission = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("now u need enter depositPercents");
                        Console.WriteLine("enter count of percents");
                        count = Convert.ToInt32(Console.ReadLine());
                        var depositPercents = new Dictionary<decimal, decimal>();
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine("enter sum and then enter percent");
                            sum = Convert.ToDecimal(Console.ReadLine());
                            percent = Convert.ToDecimal(Console.ReadLine());
                            depositPercents.Add(sum, percent);
                        }

                        Console.WriteLine("now u need create depositTerm");
                        Console.WriteLine("enter count of years, > 1");
                        years = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("enter count of months, > 1");
                        months = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("enter count of days, > 1");
                        days = Convert.ToInt32(Console.ReadLine());
                        var depositTerm = new DateTime(years, months, days);

                        bs.AddBank(new Bank(bankName, trustFactorLimit, debitPercent, creditLimit, creditCommission, depositPercents, depositTerm));
                        break;

                    case "3":
                        Console.WriteLine("what kind of account do u want to create\n1 - Debit, 2 - Credit, 3 - Deposit");
                        accountType = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("for what client do u want to create account, please enter name and suranme");
                        name = Console.ReadLine();
                        surname = Console.ReadLine();

                        Console.WriteLine("enter bank");
                        bankName = Console.ReadLine();

                        foreach (var b in bs.GetBanks())
                        {
                            if (b.GetBankName() == bankName)
                            {
                                bank = b;
                            }
                        }

                        foreach (var c in clients)
                        {
                            if (c.GetName() == name && c.GetSurname() == surname)
                            {
                                client = c;
                            }
                        }

                        bs.AddAccountToBank(client, bank, (AccountType)accountType);
                        break;

                    case "4":
                        Console.WriteLine("what operation do u want to make");
                        Console.WriteLine("Transfer - 1, WithDraw - 2, TopUp - 3, 4 - CancelOperation");
                        choice = Console.ReadLine();

                        Console.WriteLine("for what client do u want to create account, please enter name and suranme");
                        name = Console.ReadLine();
                        surname = Console.ReadLine();
                        foreach (var c in clients)
                        {
                            if (c.GetName() == name && c.GetSurname() == surname)
                            {
                                client = c;
                            }
                        }

                        Console.WriteLine("enter bank");
                        bankName = Console.ReadLine();
                        bank = bs.GetBanks().First(b => b.GetBankName() == bankName);

                        Console.WriteLine("on what account do u want to do this operation, enter account_number");
                        foreach (IAccount ac in bs.GetBanks().SelectMany(b => b.GetClientAccounts()[client]))
                        {
                            Console.WriteLine(ac.GetAccountNumber());
                        }

                        accountNumber = Console.ReadLine();
                        account = bank.GetClientAccounts()[client].First(ac => ac.GetAccountNumber() == accountNumber);

                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine("on what account do u want to transfer, enter account_number");
                                IAccount account2 = null;
                                var bank2 = new Bank();
                                foreach (var b in bs.GetBanks())
                                {
                                    foreach (var kvp in b.GetClientAccounts())
                                    {
                                        foreach (var ac in kvp.Value)
                                        {
                                            Console.WriteLine(ac.GetAccountNumber());
                                        }
                                    }
                                }

                                string accountNumber2 = Console.ReadLine();
                                foreach (var b in bs.GetBanks())
                                {
                                    foreach (var kvp in bank.GetClientAccounts())
                                    {
                                        foreach (var ac in kvp.Value)
                                        {
                                            if (ac.GetAccountNumber() != accountNumber2) continue;
                                            bank2 = b;
                                            account2 = ac;
                                        }
                                    }
                                }

                                Console.WriteLine("enter sum");
                                money = Convert.ToInt32(Console.ReadLine());

                                bs.Transfer(client, bank, account, account2, bank2, money);
                                break;
                            case "2":
                                Console.WriteLine("enter sum");
                                money = Convert.ToInt32(Console.ReadLine());
                                bs.WithDraw(client, bank, account, money);
                                break;
                            case "3":
                                Console.WriteLine("enter sum");
                                money = Convert.ToInt32(Console.ReadLine());
                                bs.TopUp(client, bank, account, money);
                                break;
                        }

                        break;
                    case "5":
                        time.SkipDay();
                        Console.WriteLine(time.GetTime());
                        break;
                    case "6":
                        time.SkipMonth();
                        Console.WriteLine(time.GetTime());
                        break;
                    case "7":
                        time.SkipYear();
                        Console.WriteLine(time.GetTime());
                        break;
                    case "8":
                        Console.WriteLine("for what client do you want to add passportID or address");
                        name = Console.ReadLine();
                        surname = Console.ReadLine();
                        client = clients.First(c => c.GetName() == name && c.GetSurname() == surname);

                        Console.WriteLine("what do u want to add");
                        Console.WriteLine("1 - address, 2 - passportId");
                        choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                address = Console.ReadLine();
                                client.SetAddress(address);
                                break;
                            case "2":
                                passportId = Console.ReadLine();
                                client.SetPassportId(passportId);
                                break;
                        }

                        break;
                    case "9":
                        Console.WriteLine("what do u want to edit");
                        Console.WriteLine("1 - debitPercent, 2 - creditLimit, 3 - depositPercent, 4 - trustFactorLimit");
                        choice = Console.ReadLine();

                        Console.WriteLine("for what bank do u want to edit");
                        Console.WriteLine("enter bankName");
                        bankName = Console.ReadLine();
                        bank = bs.GetBanks().First(b => b.GetBankName() == bankName);

                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine("enter debitPercent");
                                debitPercent = Convert.ToDecimal(Console.ReadLine());
                                bank.ChangeDebitPercent(debitPercent);
                                break;
                            case "2":
                                Console.WriteLine("enter creditLimit");
                                creditLimit = Convert.ToDecimal(Console.ReadLine());
                                bank.ChangeCreditLimit(creditLimit);
                                break;
                            case "3":
                                Console.WriteLine("enter count of percents");
                                count = Convert.ToInt32(Console.ReadLine());
                                var newDepositPercents = new Dictionary<decimal, decimal>();
                                for (int i = 0; i < count; i++)
                                {
                                    Console.WriteLine("enter sum and then enter percent");
                                    sum = Convert.ToDecimal(Console.ReadLine());
                                    percent = Convert.ToDecimal(Console.ReadLine());
                                    newDepositPercents.Add(sum, percent);
                                    bank.ChangeDepositPercents(newDepositPercents);
                                }

                                break;
                            case "4":
                                Console.WriteLine("enter trustFactorLimit");
                                trustFactorLimit = Convert.ToDecimal(Console.ReadLine());
                                bank.ChangeCreditLimit(trustFactorLimit);
                                break;
                        }

                        break;
                }
            }
        }
    }
}
