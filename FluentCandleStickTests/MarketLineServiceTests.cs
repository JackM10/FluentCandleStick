using FluentCandleStick.Models;
using FluentCandleStick.Services;

namespace FluentCandleStickTests
{
    public class MarketLineServiceTests
    {
        private MarketLineService _service;
        [SetUp]
        public void Setup()
        {
            _service = new MarketLineService();
        }

        [Test]
        public void ProcessMarketLines_ShouldGroupRecords_WhenCorrectValuesProvided()
        {
            var testDate1 = DateTime.Now.AddSeconds(1); //ToDo: consider to use datetime provider instead
            var testDate2 = DateTime.Now.AddSeconds(2);
            var inputValue = new List<MarketLine>
            {
                new MarketLine(
                    time: testDate1,
                    quantity: 1,
                    price: 1),
                new MarketLine(
                    time: testDate1,
                    quantity: 2,
                    price: 2),
                new MarketLine(
                    time: testDate2,
                    quantity: 3,
                    price: 3),
                new MarketLine(
                    time: testDate2,
                    quantity: 4,
                    price: 4),
            };
            var expectedResult = new List<MarketLineViewModel>
            {
                new MarketLineViewModel
                {
                    Open = 1,
                    Close = 2,
                    High = 2,
                    Low = 1,
                    SumVolume = 5,
                    Time = testDate1
                },
                new MarketLineViewModel
                {
                    Open = 3,
                    Close = 4,
                    High = 4,
                    Low = 3,
                    SumVolume = 25,
                    Time = testDate2
                }
            };

            var actualValue = _service.ProcessMarketLines(inputValue).ToList();

            Assert.That(actualValue.Count, Is.EqualTo(2));
            Assert.That(actualValue[0].Time, Is.EqualTo(expectedResult[0].Time)); //ToDo: implement equeality to compare just classes
            Assert.That(actualValue[0].Open, Is.EqualTo(expectedResult[0].Open));
            Assert.That(actualValue[0].Close, Is.EqualTo(expectedResult[0].Close));
            Assert.That(actualValue[0].High, Is.EqualTo(expectedResult[0].High));
            Assert.That(actualValue[0].Low, Is.EqualTo(expectedResult[0].Low));
            Assert.That(actualValue[0].SumVolume, Is.EqualTo(expectedResult[0].SumVolume));
            Assert.That(actualValue[1].Time, Is.EqualTo(expectedResult[1].Time));
            Assert.That(actualValue[1].Open, Is.EqualTo(expectedResult[1].Open));
            Assert.That(actualValue[1].Close, Is.EqualTo(expectedResult[1].Close));
            Assert.That(actualValue[1].High, Is.EqualTo(expectedResult[1].High));
            Assert.That(actualValue[1].Low, Is.EqualTo(expectedResult[1].Low));
            Assert.That(actualValue[1].SumVolume, Is.EqualTo(expectedResult[1].SumVolume));
        }
    }
}