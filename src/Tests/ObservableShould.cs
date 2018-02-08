using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Xunit;

namespace Tests
{
    public class ObservableShould
    {
        [Fact]
        public void ConvertNextSignalsToUnitWhenCallingToSignal()
        {
            var result = new[] {1, 2, 3, 4}.ToObservable()
                .ToSignal()
                .ToList()
                .Wait();
            
            Assert.All(result, value => Assert.IsType<Unit>(value));
        }

        [Fact]
        public void ReturnASingleValueWhenErrorSignalIsSentWhenCallingCatchAndReturn()
        {
            var result = Observable.Return(-1)
                .SelectMany(_ => Observable.Throw<int>(new Exception()))
                .CatchAndReturn(0)
                .Wait();
            
            Assert.Equal(0, result);
        }

        [Fact]
        public void FilterOutSignalsWithOppositeValueWhenCallingWhereWithBool()
        {
            var result = new[] {true, false, true, false, true, false}.ToObservable()
                .Where(true)
                .ToList()
                .Wait();
            
            Assert.All(result, value => Assert.True(value));
        }

        [Fact]
        public void FilterOutSignalsNotEqualToSpecifiedConstantWhenCallingWhereWithGeneric()
        {
            var stringResult = new[] {"One", "Two", "Three", "Four"}.ToObservable()
                .Where("One")
                .ToList()
                .Wait();
            
            Assert.All(stringResult, value => Assert.Equal("One", value));
        }

        [Fact]
        public void WhereNotNullShouldFilterOutValuesThatAreNull()
        {
            var result = new[] {new object(), null, new object(), null}.ToObservable()
                .WhereNotNull()
                .ToList()
                .Wait();
            
            Assert.All(result, value => Assert.NotNull(value));
        }
    }
}