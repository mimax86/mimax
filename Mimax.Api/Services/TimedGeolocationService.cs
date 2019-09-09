using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Mimax.Api.Services
{
    public class TimedGeolocationService : IGeolocationService
    {
        public TimedGeolocationService()
        {
            var timer = new System.Timers.Timer(1000) { Enabled = true };
            var ticks = Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(
                handler => (s, a) => handler(s, a),
                handler => timer.Elapsed += handler,
                handler => timer.Elapsed -= handler);
            ticks.Subscribe(data => Trace.WriteLine("OnNext: " + data.EventArgs.SignalTime));

            Func<EventHandler<ElapsedEventArgs>, ElapsedEventHandler> func = Func; 
            {

            }
            
        }

        private ElapsedEventHandler Func(EventHandler<ElapsedEventArgs> handler)
        {
            return delegate(object sender, ElapsedEventArgs args) { handler(sender, args); };
        }

        public Task<Geolocation> GetGeolocationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<int> RequestInfos { get; }

        delegate Int32 Morpher<TResult, TArgument>(Int32 startValue, TArgument argument,
            out TResult morphResult);

        static TResult Morph<TResult, TArgument>(ref Int32 target, TArgument argument,
            Morpher<TResult, TArgument> morpher)
        {
            TResult morphResult;
            Int32 currentVal = target, startVal, desiredVal;
            do
            {
                startVal = currentVal;
                desiredVal = morpher(startVal, argument, out morphResult);
                currentVal = Interlocked.CompareExchange(ref target, desiredVal, startVal);
            } while (startVal != currentVal);
            return morphResult;
        }
    }
}