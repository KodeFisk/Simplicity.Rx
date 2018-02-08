using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace System.Reactive.Linq
{
    public static class ObservableMixins
    {
        /// <summary>
        /// Converts an <c>IObservable&lt;T&gt;</c> to an <c>IObservable&lt;Unit&gt;</c>.
        /// </summary>
        /// <returns>An <c>IObservable&lt;Unit&gt;</c></returns>
        /// <param name="This">The source observable.</param>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        public static IObservable<Unit> ToSignal<T>(this IObservable<T> This) => This.Select(_ => Unit.Default);

        /// <summary>
        /// Project every item into an observable and subscribes only to the most recent one. Like <c>SelectMany</c> but uses <c>Switch</c> instead of <c>Merge</c>.
        /// </summary>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        /// <typeparam name="TResult">The type of the resulting observable.</typeparam>
        /// <param name="This">The observable to project.</param>
        /// <param name="selector">The selector to use when projecting each item.</param>
        /// <returns>An <c>IObservable&lt;TResult&gt;</c></returns>
        public static IObservable<TResult> SelectSwitch<T, TResult>(this IObservable<T> This, Func<T, IObservable<TResult>> selector) => This.Select(selector).Switch();

        /// <summary>
        /// Project every item and it's index into an observable and subscribes only to the most recent one. Like <c>SelectMany</c> but uses <c>Switch</c> instead of <c>Merge</c>.
        /// </summary>
        /// <typeparam name="T">The type of the source observable.</typeparam>
        /// <typeparam name="TResult">The type of the resulting observable.</typeparam>
        /// <param name="This">The observable to project.</param>
        /// <param name="selector">The selector to use when projecting each item.</param>
        /// <returns>An <c>IObservable&lt;TResult&gt;</c></returns>
        public static IObservable<TResult> SelectSwitch<T, TResult>(this IObservable<T> This, Func<T, int, IObservable<TResult>> selector) => This.Select(selector).Switch();

        /// <summary>
        /// Catches any error that might occur and return an observable with a single given value.
        /// </summary>
        /// <returns>An observable with a single given value.</returns>
        /// <param name="This">The source observable to catch errors on.</param>
        /// <param name="returnValue">The value to be sent in the returning observable.</param>
        /// <typeparam name="T">The type of the signal inside the observable.</typeparam>
        public static IObservable<T> CatchAndReturn<T>(this IObservable<T> This, T returnValue) => This.Catch(Observable.Return(returnValue));

        /// <summary>
        /// Syntactic sugar for filtering an <c>IObservable&lt;bool&gt;</c> to only contain <c>true</c> or <c>false</c> signals.
        /// </summary>
        /// <returns>An <c>IObservable&lt;bool&gt;</c> that only contains the specified boolean value.</returns>
        /// <param name="This">The observable to filter.</param>
        /// <param name="condition">The boolean value to check against.</param>
        public static IObservable<bool> Where(this IObservable<bool> This, bool condition) => This.Where(value => value == condition);

        /// <summary>
        /// Syntactic sugar for filtering an observable to only contain the specified constant.
        /// </summary>
        /// <param name="This">The observable to filter.</param>
        /// <param name="condition">A constant to check the values of the observable against.</param>
        /// <typeparam name="T">The type of the signals of the observable</typeparam>
        /// <returns>An observable that only contains values equal to the specified constant.</returns>
        public static IObservable<T> Where<T>(this IObservable<T> This, T condition) => This.Where(value => value.Equals(condition));

        /// <summary>
        /// Syntactic sugar for filtering an <c>IObservable&lt;T&gt;</c> to only contain non-null singals.
        /// </summary>
        /// <returns>An <c>IObservable&lt;T&gt;</c> that only contains non-null signals.</returns>
        /// <param name="This">The observable to filter.</param>
        /// <typeparam name="T">The the of the signal inside the observable.</typeparam>
        public static IObservable<T> WhereNotNull<T>(this IObservable<T> This) => This.Where(value => value != null);
    }
}
