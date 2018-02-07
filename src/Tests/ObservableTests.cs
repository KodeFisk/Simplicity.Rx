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
            var result = new List<object>();

            Observable.Range(0, 5)
                .ToSignal()
                .Subscribe(value => result.Add(value));
            
            Assert.True(result.All(value => value.GetType() == typeof(Unit)));
        }

        [Fact]
        public void WhereWithBoolShouldFilterOutSignalsWithOppositeValue()
        {
            IList<bool> result = null;

            new[] {true, false, true, false, true, false}.ToObservable()
                .Where(true)
                .ToList()
                .Subscribe(value => result = value);
            
            Assert.True(result.All(value => value == true));
        }
    }
}