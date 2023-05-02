using FluentCandleStick.Models;

namespace FluentCandleStick.Services
{
    public class MarketLineService
    {
        private static readonly string nameFileCsv = "MarketDataTest.csv";

        //ToDo: if DI - extract XML comments into interface

        /// <summary>
        /// Get values from CSV file "MarketDataTest.csv", which must be in the root of executable
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MarketLine> GetMarketLines()
        {
            var dataFromCsv = MarketLine.GetMarketListFromCsv(nameFileCsv);

            return dataFromCsv;
        }

        /// <summary>
        /// Processing market lines records - grouping by seconds and filtering by value
        /// </summary>
        /// <param name="marketLines">input values without type definition</param>
        /// <returns></returns>
        public IEnumerable<MarketLineViewModel> ProcessMarketLines(IEnumerable<MarketLine> marketLines)
        {
            var result = new List<MarketLineViewModel>();
            var marketLineGroupedBySeconds = marketLines.GroupBy(ml => ml.Time.Second).ToList();
            MarketLineViewModel marketLineViewModel;
            foreach (var marketLinesBySecond in marketLineGroupedBySeconds)
            {
                marketLineViewModel = new MarketLineViewModel
                {
                    Open = marketLinesBySecond.First().Price,
                    Close = marketLinesBySecond.Last().Price,
                    High = marketLinesBySecond.Max(i => i.Price),
                    Low = marketLinesBySecond.Min(i => i.Price),
                    SumVolume = marketLinesBySecond.Sum(i => i.Price * i.Quantity),
                    Time = marketLinesBySecond.First().Time //ToDo: Not sure which date should be taken
                };

                result.Add(marketLineViewModel); //ToDo: should we return grouped values or all values?
            }

            return result;
        }
    }
}
