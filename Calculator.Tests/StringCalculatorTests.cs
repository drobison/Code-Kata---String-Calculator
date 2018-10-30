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
        public void StringCalculator_AlwaysReturn0()
        {
            var input = "";
            var result = _sut.Add(input);
            Assert.AreEqual(0, result);
        }
    }
}
