using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Xunit;

namespace Tests
{
    public class ObserverShould
    {
        [Fact]
        public void SendOneNextSignalAndThenACompletedSignalWhenUsingCompleteWith()
        {
            const string next = "Next";
            const string error = "Error";
            const string completed = "Completed";

            var result = new List<string>();

            var observer = Observer.Create<Unit>(
                value => result.Add(next),
                ex => result.Add(error),
                () => result.Add(completed));

            observer.CompleteWith(Unit.Default);

            Assert.Equal(new[] {next, completed}, result);
        }
    }
}