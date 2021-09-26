using System;
namespace Shops.Tools
{
    public class ShopsExeption : Exception
    {
        public ShopsExeption()
        {
        }

        public ShopsExeption(string message)
            : base(message)
        {
        }

        public ShopsExeption(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}