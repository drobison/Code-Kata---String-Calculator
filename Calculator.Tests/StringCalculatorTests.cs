﻿using System;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private StringCalculator _sut { get; set; }

        [SetUp]
        public void Setup()
        {
            _sut = new StringCalculator();
        }

        [Test]
        public void Add_EmptyString_Return0()
        {
            var input = "";
            var result = _sut.Add(input);
            Assert.AreEqual(0, result);
        }

        [TestCase("1", 1)]
        [TestCase("5",5)]
        [TestCase("10", 10)]
        [TestCase("103", 103)]
        public void Add_OneNumber_ReturnNumber(string input, int expectedResult)
        {
            var result = _sut.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1,2", 3)]
        [TestCase("5,10", 15)]
        [TestCase("100,100", 200)]
        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,4,5,6,7", 28)]
        public void Add_MultipleNumbers_ReturnResult(string input, int expectedResult)
        {
            var result = _sut.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1\n2", 3)]
        [TestCase("1\n2,3", 6)]
        [TestCase("1\n2\n3", 6)]
        public void Add_NewLinesCanBeSeperators_ReturnResult(string input, int expectedResult)
        {
            var result = _sut.Add(input);
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase("//;\n1;2", 3)]
        [TestCase("//;\n1;2\n3", 6)]
        public void Add_ChangeSeperators_ReturnResult(string input, int expectedResult)
        {
            var result = _sut.Add(input);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("1,-2", "negatives not allowed -2")]
        [TestCase("-1,2", "negatives not allowed -1")]
        [TestCase("-1,-2", "negatives not allowed -1, -2")]
        [TestCase("//;\n1;-2", "negatives not allowed -2")]
        public void Add_WithNegatives_ThrowError(string input, string expectedMessage)
        {
            var ex = Assert.Throws<Exception>(() => _sut.Add(input));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [TestCase("//;\n1001;2", 2)]
        [TestCase("5001,139", 139)]
        [TestCase("1000,999", 1999)]
        public void Add_NumbersOver1000_ShouldBeIgnoredInResult(string input, int expectedResult)
        {
            var result = _sut.Add(input);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
