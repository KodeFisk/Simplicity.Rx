namespace System
{
    public static class IObserverMixins
    {
        /// <summary>
        /// Invokes <c>OnNext</c> with the given value, and then invokes <c>OnCompleted</c>
        /// </summary>
        /// <param name="This">The <c>IObserver</c> to complete</param>
        /// <param name="value">The last value to be emitted to the <c>IObserver</c></param>
        /// <typeparam name="T">The type of the signals handled by the <c>IObserver</c></typeparam>
        public static void CompleteWith<T>(this IObserver<T> This, T value)
        {
            This.OnNext(value);
            This.OnCompleted();
        }
    }
}