using ChangeApi.Domain.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ChageApi.Tests
{
    public class ChangeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenKnownMonetaryUnitsOfBillAndPaymentShouldReturnExpectCountOfNotesAndCoins()
        {
            var expectNotes = new Dictionary<int, int>();
            expectNotes.Add(20, 1);
            expectNotes.Add(10, 1);

            var expectCoins = new Dictionary<int, int>();
            expectCoins.Add(50, 2);

            var change = new Change();
            var stringResult = change.Calc(81, 50);


            Assert.IsTrue(expectNotes.Keys.All(x => change.Notes.ContainsKey(x)));
            Assert.IsTrue(expectNotes.All(x => change.Notes[x.Key] == x.Value));

            Assert.IsTrue(expectCoins.Keys.All(x => change.Coins.ContainsKey(x)));
            Assert.IsTrue(expectCoins.All(x => change.Coins[x.Key] == x.Value));
        }
    }
}