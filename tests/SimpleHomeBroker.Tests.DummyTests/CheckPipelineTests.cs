using System;
using NUnit.Framework;

namespace SimpleHomeBroker.Tests.DummyTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOne()
        {
            Assert.True(DateTimeOffset.Now.ToUnixTimeMilliseconds() > 0);
        }

        [Test]
        public void TestTwo()
        {
            Assert.True(DateTimeOffset.Now.ToUnixTimeMilliseconds() > 0);
        }
    }
}