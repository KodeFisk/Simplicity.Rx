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

        [Fact]
        public void WhereWithGenericShouldFilterOutSignalsNotEqualToSpecifiedConstant()
        {
            IList<string> stringResult = null;
            IList<int> intResult = null;

            new[] {"One", "Two", "Three", "Four"}.ToObservable()
                .Where("One")
                .ToList()
                .Subscribe(value => stringResult = value);

            new[] {1, 2, 3, 4}.ToObservable()
                .Where(1)
                .ToList()
                .Subscribe(value => intResult = value);
            
            Assert.True(stringResult.All(value => value == "One"));
            Assert.True(intResult.All(value => value == 1));
        }
    }
}