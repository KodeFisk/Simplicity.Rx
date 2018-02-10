# Simplicity.Rx

A library of extension methods to make Rx.NET code easier to write and read.

## Operators

Though they are in fact extension methods, the methods inlcuded in Simplicity.Rx are called operators, as they can be used as any other Rx operator.

### ToSignal

Converts an `IObservable<T>` to `IObservable<Unit>`.

```C#
sourceObservable
    .ToSignal()
    ...
```

### SelectLatest

Works like `SelectMany` but only subscribes to the most recent `IObservable<T>` produced.

```C#
sourceObservable
    .SelectLatest(_ => Observable.Interval(TimeSpan.FromSeconds(1)))
    ...
```



### CatchAndReturn

Intercepts the `OnError` signal and returns the specified value instead.

```c#
sourceObservable
    .CatchAndReturn("Something went wrong")
    ...
```



### Where

Let's you specify a constant value, instead of a predicate.

```
sourceObservable
    .Where(true)
    ...
	
otherObservable
	.Where("What I want")
	...
```

### WhereNotNull

Only let's non-null values through.

```C#
sourceObservable
    .WhereNotNull()
    ...
```

### GetEventSignal

Returns an `IObservable<Unit>` which emits a signal every time the event fires.

```C#
anyObject.GetEventSignal(nameof(anyObject.EventName))
    ...
```

### GetEvents

Returns an `IObservable<EventPattern<TEventArgs` which emits a signal every time the event fires.

```C#
anyObject.GetEvents<EventArgs>(nameof(anyObject.EventName))
    ...
```

## Convenience methods

These methods don't work as operators do, but still provide a convenient shortcuts to common tasks.

### CompleteWith

Emit an `OnNext` signal with the specified value, followed by an `OnCompleted` signal, effectively ending the observable.

```
anyObserver.CompleteWith("Done");
```



