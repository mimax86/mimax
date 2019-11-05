using System;
using System.Collections.Generic;
using System.Text;

namespace Mimax.Interoperability.CSharpProject
{
    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void PrintName() => Console.WriteLine($"My name is {Name}");
    }
}
