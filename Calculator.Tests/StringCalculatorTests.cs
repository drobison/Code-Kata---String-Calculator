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
    }
}
