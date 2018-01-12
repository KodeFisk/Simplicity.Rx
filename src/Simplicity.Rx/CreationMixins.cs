using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace System.Reactive.Linq
{
    public static class CreationMixins
    {
        /// <summary>
        /// Creates an observable that signals every time the event with the given name is fired
        /// </summary>
        /// <returns>An observable that signals every time the event is fired.</returns>
        /// <param name="source">The object containing the event.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <typeparam name="T">The type of the class contains the event.</typeparam>
        public static IObservable<EventPattern<object>> GetEvents<T>(this T source, string eventName) => Observable.FromEventPattern(source, eventName);

        /// <summary>
        /// Creates an observable that signals every time the event with the given name is fired
        /// </summary>
        /// <returns>An observable that signals ever time the event is fired.</returns>
        /// <param name="source">The object containing the event.</param>
        /// <param name="eventName">Name of the event.</param>
        /// <typeparam name="TEventArgs">The type of event arguments emitted by the event.</typeparam>
        public static IObservable<EventPattern<TEventArgs>> GetEvents<TEventArgs>(this object source, string eventName) => Observable.FromEventPattern<TEventArgs>(source, eventName);
    }
}
