using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace System.Reactive
{
    public static class SystemReactiveMixins
    {
        /// <summary>
        /// Converts an <c>IObservable&lt;T&gt;</c> to an <c>IObservable&lt;Unit&gt;</c>.
        /// </summary>
        /// <returns>An <c>IObservable&lt;Unit&gt;</c></returns>
        /// <param name="source">The source observable.</param>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        public static IObservable<Unit> ToSignal<T>(this IObservable<T> source) => source.Select(_ => Unit.Default);

        /// <summary>
        /// Catches any error that might occur and return an observable with a single given value.
        /// </summary>
        /// <returns>An observable with a single given value.</returns>
        /// <param name="source">The source observable to catch errors on.</param>
        /// <param name="returnValue">The value to be sent in the returning observable.</param>
        /// <typeparam name="T">The type of the signal inside the observable.</typeparam>
        public static IObservable<T> CatchAndReturn<T>(this IObservable<T> source, T returnValue) => source.Catch(Observable.Return(returnValue));
    }
}
