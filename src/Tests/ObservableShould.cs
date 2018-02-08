using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Reactive.Testing;
using Xunit;

namespace Tests
{
    public class ObservableShould
    {
        private TestScheduler _scheduler;

        public ObservableShould()
        {
            _scheduler = new TestScheduler();
        }
        
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
        public void OnlySubscribeToMostRecentObservableWhenCallingSelectLatestAndReturningAnObservable()
        {
            var result = -1L; 
                
            Observable.Interval(TimeSpan.FromSeconds(.2), _scheduler)
                .Take(5)
                .SelectLatest(value => Observable.Timer(TimeSpan.FromSeconds(1), _scheduler)
                    .Select(_ => value))
                .Subscribe(value => result = value);

            _scheduler.Start();
            
            Assert.Equal(4L, result);
        }

        [Fact]
        public void OnlySubscribeToMostRecentObservableWhenCallingSelectLatestAndReturningATask()
        {
            var result = Observable.Interval(TimeSpan.FromSeconds(.2))
                .Take(5)
                .SelectLatest(async value =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    return value;
                })
                .Wait();
            
            Assert.Equal(4L, result);
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