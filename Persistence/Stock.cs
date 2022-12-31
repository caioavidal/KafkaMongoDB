using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{

    [BsonCollection("stock")]
    public class Stock: Document
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }

        private static List<(string, string)> _stocks = new List<(string, string)>
            {
                ("AAPL","Apple Inc. Common Stock"),
                ("MSFT","Microsoft Corporation Common Stock"),
                ("GOOG","Alphabet Inc. Class C Capital Stock"),
                ("GOOGL","Alphabet Inc. Class A Common Stock"),
                ("AMZN","Amazon.com, Inc. Common Stock"),
                ("BRK/A","Berkshire Hathaway Inc."),
                ("BRK/B","Berkshire Hathaway Inc."),
                ("UNH","UnitedHealth Group Incorporated Common Stock (DE)"),
                ("JNJ","Johnson & Johnson Common Stock"),
                ("XOM","Exxon Mobil Corporation Common Stock"),
                ("V","Visa Inc."),
                ("JPM","JP Morgan Chase & Co. Common Stock"),
                ("TSLA","Tesla, Inc. Common Stock"),
                ("TSM","Taiwan Semiconductor Manufacturing Company Ltd."),
                ("WMT","Walmart Inc. Common Stock"),
                ("PG","Procter & Gamble Company (The) Common Stock"),
                ("NVDA","NVIDIA Corporation Common Stock"),
                ("LLY","Eli Lilly and Company Common Stock")
            };
        public static Stock CreateRandomStock()
        {
            var random = new Random();
            var index = random.Next(0, _stocks.Count - 1);

            return new Stock
            {
                Code = _stocks[index].Item1,
                Name = _stocks[index].Item2,
                Price = Math.Round((decimal)random.NextDouble() * 100, 2),
            };
        }
    }
}
