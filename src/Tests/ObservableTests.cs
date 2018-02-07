using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Xunit;

namespace Tests
{
    public class ObservableTests
    {
        [Fact]
        public void ToSignalShouldConvertNextSignalsToUnit()
        {
            var result = new[] {1, 2, 3, 4}.ToObservable()
                .ToSignal()
                .ToList()
                .Wait();
            
            Assert.True(result.All(value => value.GetType() == typeof(Unit)));
        }

        [Fact]
        public void CatchAndReturnShouldReturnASingleValueWhenErrorSignalIsSent()
        {
            var result = Observable.Return(-1)
                .SelectMany(_ => Observable.Throw<int>(new Exception()))
                .CatchAndReturn(0)
                .Wait();
            
            Assert.True(result == 0);
        }

        [Fact]
        public void WhereWithBoolShouldFilterOutSignalsWithOppositeValue()
        {
            var result = new[] {true, false, true, false, true, false}.ToObservable()
                .Where(true)
                .ToList()
                .Wait();
            
            Assert.True(result.All(value => value == true));
        }

        [Fact]
        public void WhereWithGenericShouldFilterOutSignalsNotEqualToSpecifiedConstant()
        {
            var stringResult = new[] {"One", "Two", "Three", "Four"}.ToObservable()
                .Where("One")
                .ToList()
                .Wait();

            var intResult = new[] {1, 2, 3, 4}.ToObservable()
                .Where(1)
                .ToList()
                .Wait();
            
            Assert.True(stringResult.All(value => value == "One"));
            Assert.True(intResult.All(value => value == 1));
        }

        [Fact]
        public void WhereNotNullShouldFilterOutValuesThatAreNull()
        {
            var result = new[] {new object(), null, new object(), null}.ToObservable()
                .WhereNotNull()
                .ToList()
                .Wait();
            
            Assert.True(result.All(value => value != null));
        }
    }
}