using System.Globalization;

namespace FluentCandleStick.Models
{
    public class MarketLine
    {
        private const string TIME_FORMAT = "dd/MM/yyyy HH:mm:ss.fff";
        private static readonly char DELIMITER = ',';

        public DateTime Time { get; }
        public long Quantity { get; }
        public decimal Price { get; }

        public MarketLine(DateTime time, long quantity, decimal price)
        {
            Time = time;
            Quantity = quantity;
            Price = price;
        }

        public static IEnumerable<MarketLine> GetMarketListFromCsv(string nameFileCsv)
        {
            IEnumerable<MarketLine> marketList = File.ReadAllLines(nameFileCsv)
                   .Skip(1)
                   .Select(csvLine =>
                   {
                       string[] arrLine = csvLine.Split(DELIMITER);
                       var marketLine = new MarketLine(ParseDate(arrLine[0]), long.Parse(arrLine[1]), ParseDecimal(arrLine[2]));
                       return marketLine;
                   });

            return marketList;
        }

        private static DateTime ParseDate(string arrLine)
        {
            return DateTime.ParseExact(arrLine, TIME_FORMAT, CultureInfo.InvariantCulture);
        }

        private static decimal ParseDecimal(string arrLine)
        {
            return decimal.Parse(arrLine.Replace('.', ','));
        }
    }
}