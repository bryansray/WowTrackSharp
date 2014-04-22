using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using LootTrack.Domain.Models;
using NUnit.Framework;

namespace LootTrack.Domain.Tests
{
    [TestFixture]
    public class LootTests
    {
        [Test]
        public void ShouldConsiderLootedItemsEqual()
        {
            var receivedAt = DateTime.Now;
            var character = new Character();
            var item = new Item("Item 1", 1234, 572, 4);

            var lootedItem1 = new Loot(character, item, receivedAt, true);
            var lootedItem2 = new Loot(character, item, receivedAt, true);

            lootedItem1.Should().Be(lootedItem2);
        }
    }
}
