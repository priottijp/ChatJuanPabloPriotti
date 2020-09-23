using ChatJuanPabloPriotti.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJuanPabloPriotti.Consumer
{
    public class Consumer
    {
        public async void Consume(string receivedMessage)
        {
            await this.ReadCSV(receivedMessage);
        }

        public async Task<bool> ReadCSV(string message)
        {
            try
            {
                var user_name = message.Split(',')[0];
                var stock_code = message.Split(',')[1];
                Dictionary<string, string> stockData = new Dictionary<string, string>();
                string path = $@"https://stooq.com/q/l/?s={stock_code.ToLower()}&f=sd2t2ohlcv&h&e=csv";
                var response = new System.Net.WebClient().DownloadString(path).Split("\r\n");
                var headers = response[0];
                var headersValues = headers.Split(',');
                var data = response[1];
                var dataValues = data.Split(',');
                for (int i = 0; i < headersValues.Count() - 1; i++)
                    stockData.Add(headersValues[i], dataValues[i]);
                ChatHub ch = new ChatHub();
                string returnMessage = $"Decoupled Message: {user_name} - {stock_code.ToUpper()} quote is ${stockData.FirstOrDefault(k => k.Key == "Close").Value} per share";
                Console.WriteLine(returnMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
