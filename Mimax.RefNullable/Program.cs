#nullable enable

using System;

namespace Mimax.RefNullable
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = new Address();
            address.Building = null;
            address.Street = null;
            address.City = "London";
            address.Region = null;

            Console.WriteLine("Hello World!");
        }
    }

    class Address
    {
        public string? Building;
        public string Street = string.Empty;
        public string City = string.Empty;
        public string Region = string.Empty;
    }
}