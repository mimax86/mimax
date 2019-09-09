using FluentAssertions;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.FormattableString;

namespace Mimax.Tests
{
    [TestFixture]
    public class ClosureTests
    {
        [Test]
        public void Test()
        {
            List<Func<string>> actions = new List<Func<string>>();
            int outerCounter = 0;
            for (int i = 0; i < 2; i++)
            {
                int innerCounter = 0;
                Func<string> action = () =>
                {
                    var result = string.Format("Outer: {0}; Inner: {1}", outerCounter, innerCounter);
                    outerCounter++;
                    innerCounter++;
                    return result;
                };
                actions.Add(action);
            }

            actions[0]().Should().Be("Outer: 0; Inner: 0");
            actions[0]().Should().Be("Outer: 1; Inner: 1");
            actions[1]().Should().Be("Outer: 2; Inner: 0");
            actions[1]().Should().Be("Outer: 3; Inner: 1");
        }

        [Test]
        public void Test2()
        {
            List<Func<string>> actions = new List<Func<string>>();
            for (int i = 0; i < 2; i++)
            {
                Func<string> action = () =>
                {
                    var result = i.ToString();
                    return result;
                };
                actions.Add(action);
            }

            actions[0]().Should().Be("2");
            actions[0]().Should().Be("2");
            actions[1]().Should().Be("2");
            actions[1]().Should().Be("2");
        }

        [Test]
        public void Test3()
        {
            var n = new Nullable<int>();
            Assert.Throws<NullReferenceException>(() => n.GetType());
        }

        [Test]
        public void Test4()
        {
            Assert.Throws<RuntimeBinderException>(() =>
            {
                dynamic text = "hello world";
                var t = text.GetType();
                string world = text.Substring(6);
                Console.WriteLine(world);
                string broken = text.SUBSTR(6);
                Console.WriteLine(broken);
            });
        }

        [Test]
        public void Test5()
        {
            dynamic expando = new ExpandoObject();
            expando.SomeData = "Some data";
            Action<string> action =
                input => Console.WriteLine("The input was '{0}'", input);
            expando.FakeMethod = action;
            Console.WriteLine(expando.SomeData);
            expando.FakeMethod("hello");
            IDictionary<string, object> dictionary = expando;
            Console.WriteLine("Keys: {0}",
            string.Join(", ", dictionary.Keys));
            dictionary["OtherData"] = "other";
            Console.WriteLine(expando.OtherData);
        }

        [Test]
        public void Test6()
        {
            IEnumerable<string> strings = new List<string> { "a", "b", "c" };
            IEnumerable<object> objects = strings;

            Action<string> method = new Action<object>(t => Console.Write(t));
            method("t");

            IEnumerable<int> ints = new List<int> { 1, 2, 3 };
            //Boxing goes down
            //objects = ints;

            //Numeric conversion goes
            //IEnumerable<long> longs = ints;

            IEnumerable<S> ss = new List<S>();
            //Boxing goes down
            //objects = ss;
            var o = new S();

            //Boxing goes down
            //IEnumerable<IEquatable<S>> ssi = ss;
            IEquatable<S> s = new S();

            List<Circle> circles = new List<Circle>
            {
                new Circle(5.3),
                new Circle(2),
                new Circle(10.5)
            };
            IComparer<Shape> comparer = new AreaComparer();
            IComparer<Circle> cComparer = comparer;

            circles.Sort(cComparer);
            foreach (Circle circle in circles)
            {
                Console.WriteLine(circle.Radius);
            }
        }

        public class AreaComparer : IComparer<Shape>
        {
            public int Compare(Shape x, Shape y)
            {
                return x.Area.CompareTo(y.Area);
            }
        }

        public abstract class Shape
        {
            public abstract double Area { get; }

        }

        public class Circle : Shape
        {
            public Circle(double radius)
            {
                Radius = radius;
            }

            public double Radius { get; }

            public override double Area => Math.PI * Math.Pow(Radius, 2);
        }

        public struct S : IEquatable<S>
        {
            public bool Equals(S other)
            {
                throw new NotImplementedException();
            }
        }

        public delegate T InvalidCovariant<out T>();

        public delegate void InvalidContravariant<in T>();

        public delegate T InvalidInvariane<T>(T p);

        public interface IInvalidContravariant<in T>
        {
            void SetValue(T p);
        }

        public delegate TResult Func<in T, out TResult>(T arg);

        [Test]
        public void Test7()
        {
            Task task = DemoCompletedAsync();
            Console.WriteLine("Method returned");
            task.Wait();
            Console.WriteLine("Task completed");

            var cts = new CancellationTokenSource();
        }

        static async Task DemoCompletedAsync()
        {
            Console.WriteLine("Before first await");
            await Task.FromResult(10);
            Console.WriteLine("Between awaits");
            await Task.Delay(1000);
            Console.WriteLine("After second await");
        }

        [Test]
        public void Test8()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                List<string> names = new List<string> { "x", "y", "z" };
                var actions = new List<Action>();
                for (int i = 0; i < names.Count; i++)
                {
                    actions.Add(() => Console.WriteLine(names[i]));
                }
                foreach (Action action in actions)
                {
                    action();
                }
            });
        }

        [Test]
        public void Test9()
        {
            DateTime date = DateTime.UtcNow;
            string parameter1 = string.Format(
             CultureInfo.InvariantCulture,
             "x={0:yyyy-MM-dd}",
             date);
            string parameter2 =
             ((FormattableString)$"x={date:yyyy-MM-dd}")
             .ToString(CultureInfo.InvariantCulture);
            string parameter3 = FormattableString.Invariant(
             $"x={date:yyyy-MM-dd}");
            string parameter4 = Invariant($"x={date:yyyy-MM-dd}");
        }

        [Test]
        public void Test10()
        {
            var allContacts = new List<Person>();
            Person jon = new Person
            {
                Name = "Jon",
                Contacts =
                {
                    allContacts.Where(c => c.Town == "Reading"),
                    new Person(),
                    { new Person(), 10 },
                    { "Misha", "Hong Kong" }
                }
            };

            jon = new Person();
            jon.Name = "Jon";
            jon.Contacts = new List<Person>();
            jon.Contacts.Add(allContacts.Where(c => c.Town == "Reading"));
            jon.Contacts.Add(new Person());
            jon.Contacts.Add(new Person(), 10);

            var dictionary = new Dictionary<string, Person>();
            dictionary.Add("Jon", new Person() { Name = "Jon" });
            dictionary.Add(new Person { Name = "Jon" });

            dictionary = new Dictionary<string, Person>
            {
                new Person { Name="Ivan" }
            };
        }

        [Test]
        public void Test11()
        {
            var fibonacci = Tuples.GenerateSequence(
                (current: 0, next: 1),
                pair => (pair.next, pair.current + pair.next),
                pair => pair.current);
        }

        [Test]
        public void Test12()
        {
            var sequence = Enumerable.Range(0, 10);
            var tuples = sequence.Select((item, index) => (item, index));
            foreach (var item in tuples)
            {
                Console.WriteLine(item);
            }

            //IEnumerable<(string, string)> stringPairs = new (string, string)[10];
            //IEnumerable<(object, object)> objectPairs = stringPairs;
        }

        [Test]
        public void Test13()
        {
            var tuple = (1, "test");
            tuple.Item2 = "test2";

            ValueTuple<int, int> t;


            var (a, b) = tuple;
            (int c, string d) = tuple;
            int e;
            string f;
            (e, f) = tuple;
            Console.WriteLine($"a: {a}; b: {b}");
            Console.WriteLine($"c: {c}; d: {d}");
            Console.WriteLine($"e: {e}; f: {f}");

            var point = new Point(10, 20);
            var (x, y) = point;
            if (x > y)
                throw new InvalidOperationException();
        }

        [Test]
        public void Test14()
        {

        }

        public class Customer
        {
            public Address Address { get; set; }
        }

        public class Address
        {
            public string Country { get; set; }
        }

        static void Greet(Customer customer)
        {
            string greeting = customer switch
            {
                { Address: { Country: "UK" } } =>
                "Welcome, customer from the United Kingdom!",
                { Address: { Country: "USA" } } =>
                "Welcome, customer from the USA!",
                { Address: { Country: string country } } =>
                $"Welcome, customer from {country}!",
                { Address: { } } =>
                "Welcome, customer whose address has no country!",
                { } =>
                "Welcome, customer of an unknown address!",
                _ =>
                "Welcome, nullness my old friend!"
            };
            Console.WriteLine(greeting);

            Span<int> span = stackalloc int[] { 5, 2, 7, 8, 2, 4, 3 };

            //Index start = 2;
            //Index end = ^2;
            //Range all = ..;
            //Range startOnly = start..;
            //Range endOnly = ..end;
            //Range startAndEnd = start..end;
            //Range implicitIndexes = 1..5;
        }

        //static void PrintLength(string text!)
        //{
        //    Console.WriteLine(text.Length);
        //}
    }

    public class Point
    {
        public Point(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

        public int X { get; private set; }

        public int Y { get; private set; }
    }

    public static class Tuples
    {
        static (int min, int max) MinMax(IEnumerable<int> source)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException(
                    "Cannot find min/max of an empty sequence");
                }
                int min = iterator.Current;
                int max = iterator.Current;
                while (iterator.MoveNext())
                {
                    min = Math.Min(min, iterator.Current);
                    max = Math.Max(max, iterator.Current);
                }
                return (min, max);
            }
        }

        static IEnumerable<int> Fibonacci()
        {
            var pair = (current: 0, next: 1);
            while (true)
            {
                yield return pair.current;
                pair = (pair.next, pair.current + pair.next);
            }
        }

        static int Fib(int n)
        {
            switch (n)
            {
                case 0: return 0;
                case 1: return 1;
                case var _ when n > 1: return Fib(n - 2) + Fib(n - 1);
                default: throw new ArgumentOutOfRangeException(nameof(n), "Input must be non-negative");
            }
        }

        public static IEnumerable<TResult> GenerateSequence<TState, TResult>(TState seed,
            Func<TState, TState> generator, Func<TState, TResult> resultSelector)
        {
            var state = seed;
            while (true)
            {
                yield return resultSelector(state);
                state = generator(state);
            }
        }

    }

    static class PersonDictionaryExtensions
    {
        public static void Add(this Dictionary<string, Person> dictionary, Person person)
        {
            dictionary.Add(person.Name, person);
        }
    }

    public static class PersonExtensions
    {
        public static void Add(this List<Person> list, IEnumerable<Person> items)
        {

        }

        public static void Add(this List<Person> list, Person person, int count = 1)
        {

        }

        public static void Add(this List<Person> list, string name, string town)
        {

        }
    }

    public class Person
    {
        public string Name { get; set; }

        public string Town { get; set; }

        public List<Person> Contacts { get; set; }
    }
}
