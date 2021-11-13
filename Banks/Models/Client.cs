using System;
using Banks.Tools;

namespace Banks.Models
{
    public class Client : IEquatable<Client>
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passportId;
        private bool _subscription = false;

        public Client(string name, string surname, string address, string passportId)
        {
            if (name == null || surname == null) throw new BanksException("write name and surname");
            _name = name;
            _surname = surname;
            _address = address;
            _passportId = passportId;
        }

        public Client() { }

        public static ClientBuilder CreateBuilder()
        {
            return new ClientBuilder();
        }

        public bool Equals(Client other)
        {
            if (other == null)
                return false;
            return _passportId == other._passportId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is not Client productObj)
                return false;
            else
                return Equals(productObj);
        }

        public override int GetHashCode() => _name.GetHashCode() ^ _surname.GetHashCode();
        public void Subscription() { _subscription = true; }
        public bool GetSubscription() => _subscription;

        public void DebitPercentsUpdated()
        {
            Console.WriteLine("debit percents updated");
        }

        public void CreditLimitUpdated()
        {
            Console.WriteLine("credit percents updated");
        }

        public void TrustFactorLimitUpdated()
        {
            Console.WriteLine("trustFactorLimit updated");
        }

        public void DepositPercentsUpdated()
        {
            Console.WriteLine("deposit percents updated");
        }

        public bool TrustFactor() { return _address != null || _passportId != null; }
        public string GetName() => _name;
        public string GetSurname() => _surname;

        internal void SetName(string name) { _name = name; }
        internal void SetSurname(string surname) { _surname = surname; }
        internal void SetAddress(string address) { _address = address; }

        internal void SetPassportId(string passportId)
        {
            if (passportId.ToString().Length != 6) throw new BanksException("incorrect passportId");
            _passportId = passportId;
        }
    }
}