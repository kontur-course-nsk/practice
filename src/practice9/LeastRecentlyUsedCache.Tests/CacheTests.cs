using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace LeastRecentlyUsedCache.Tests
{
    public class CacheTests
    {
        private ICache<int, int> cache;

        [SetUp]
        public void SetUp()
        {
            this.cache = new Cache<int, int>();
        }

        [Test]
        public void Get_GotItem_WhenExists()
        {
            this.cache[1] = 1;

            this.cache[1].Should().Be(1);
        }

        [Test]
        public void Get_GotActualItem_WhenRewrite()
        {
            this.cache[1] = 1;
            this.cache[1] = 2;

            this.cache[1].Should().Be(2);
        }

        [Test]
        public void Get_ThrowKeyNotFoundException_WhenNotExists()
        {
            Action action = () => _ = this.cache[1];

            action.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void Count_Zero_WhenEmpty()
        {
            this.cache.Count.Should().Be(0);
        }

        [Test]
        public void Count_Two_WhenAddTwoItems()
        {
            this.cache[1] = 1;
            this.cache[2] = 2;

            this.cache.Count.Should().Be(2);
        }

        [Test]
        public void Count_One_WhenRewrite()
        {
            this.cache[1] = 1;
            this.cache[1] = 2;

            this.cache.Count.Should().Be(1);
        }

        [Test]
        public void Count_Zero_WhenRemoveAllItems()
        {
            this.cache[1] = 1;
            this.cache[2] = 2;
            this.cache.RemoveLeastRecentlyUsed();
            this.cache.RemoveLeastRecentlyUsed();

            this.cache.Count.Should().Be(0);
        }

        [Test]
        public void RemoveLeastRecentlyUsed_RemoveFirst_WhenAddTwo()
        {
            this.cache[1] = 1;
            this.cache[2] = 2;
            this.cache.RemoveLeastRecentlyUsed();

            Action action = () => _ = this.cache[2];

            action.Should().NotThrow();
        }

        [Test]
        public void RemoveLeastRecentlyUsed_RemoveSecond_WhenAddTwoAndReadFirst()
        {
            this.cache[1] = 1;
            this.cache[2] = 2;
            var _ = this.cache[1];
            this.cache.RemoveLeastRecentlyUsed();

            Action action = () => _ = this.cache[1];

            action.Should().NotThrow();
        }
    }
}
