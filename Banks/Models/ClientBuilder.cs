using System;
using Banks.Tools;

namespace Banks.Models
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passportId;

        public Client Create()
        {
            return new Client(_name, _surname, _address, _passportId);
        }

        public ClientBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder SetPassportId(string passportId)
        {
            _passportId = passportId;
            return this;
        }
    }
}