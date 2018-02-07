using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Xunit;

namespace Tests
{
    public class ObserverTests
    {
        [Fact]
        public void CompleteWithShouldSendOneNextSignalAndThenACompletedSignal()
        {
            const string next = "Next";
            const string error = "Error";
            const string completed = "Completed";

            var results = new List<string>();

            var observer = Observer.Create<Unit>(
                value => results.Add(next),
                ex => results.Add(error),
                () => results.Add(completed));

            observer.CompleteWith(Unit.Default);

            Assert.True(results.SequenceEqual(new[] {next, completed}));
        }
    }
}