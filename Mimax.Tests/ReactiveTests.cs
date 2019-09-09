using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace Mimax.Tests
{
    [TestFixture]
    public class ReactiveTests
    {
        public void Test()
        {
            var subscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .AsWeakObservable()
                .SubscribeConsole("Interval");
            Console.WriteLine("Collecting");
            GC.Collect();
            Thread.Sleep(2000); //2 seconds
            GC.KeepAlive(subscription);
            Console.WriteLine("Done sleeping");
            Console.WriteLine("Collecting");
            subscription = null;
            GC.Collect();
            Thread.Sleep(2000); //2 seconds
            Console.WriteLine("Done sleeping");
        }
    }

    class WeakObserverProxy<T> : IObserver<T>
    {
        private IDisposable _subscriptionToSource;
        private WeakReference<IObserver<T>> _weakObserver;
        public WeakObserverProxy(IObserver<T> observer)
        {
            _weakObserver = new WeakReference<IObserver<T>>(observer);
        }
        internal void SetSubscription(IDisposable subscriptionToSource)
        {
            _subscriptionToSource = subscriptionToSource;
        }
        void NotifyObserver(Action<IObserver<T>> action)
        {
            IObserver<T> observer;
            if (_weakObserver.TryGetTarget(out observer))
            {
                action(observer);
            }
            else
            {
                _subscriptionToSource.Dispose();
            }
        }
        public void OnNext(T value)
        {
            NotifyObserver(observer => observer.OnNext(value));
        }
        public void OnError(Exception error)
        {
            NotifyObserver(observer => observer.OnError(error));
        }
        public void OnCompleted()
        {
            NotifyObserver(observer => observer.OnCompleted());
        }

        public IDisposable AsDisposable()
        {
            return _subscriptionToSource;
        }
    }

    public static class ObservableExtensions
    {
        public static IObservable<T> AsWeakObservable<T>(this IObservable<T> source)
        {
            return Observable.Create<T>(o =>
            {
                var weakObserverProxy = new WeakObserverProxy<T>(o);
                var subscription = source.Subscribe(weakObserverProxy);
                weakObserverProxy.SetSubscription(subscription);
                return weakObserverProxy.AsDisposable(); ;
            });
        }
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name = "")
        {
            return observable.Subscribe(new ConsoleObserver<T>(name));
        }
    }

    public class ConsoleObserver<T> : IObserver<T>
    {
        private readonly string _name;
        public ConsoleObserver(string name = "")
        {
            _name = name;
        }
        public void OnNext(T value)
        {
            Console.WriteLine("{0} - OnNext({1})", _name, value);
        }
        public void OnError(Exception error)
        {
            Console.WriteLine("{0} - OnError:", _name);
            Console.WriteLine("\t {0}", error);
        }
        public void OnCompleted()
        {
            Console.WriteLine("{0} - OnCompleted()", _name);
        }
    }
}
