﻿using ChatJuanPabloPriotti.Core.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatJuanPabloPriotti.Hubs
{
    public class ChatHub : Hub, IChatHub
    {

        public async Task SendMessage(string user, string message)
        => await Clients.All.SendAsync("ReceiveMessage", $"{DateTime.Now} {user}", message);

        public bool CheckLogin()
        => this.Context.User.Identity.IsAuthenticated;

        public string GetCurrentUser()
            => this.Context.User.Identity.Name;

        public void ReadCSV(string stock_code)
         =>   //Decoupled
            Producer.Producer.PostMessage($"{this.Context.User.Identity.Name},{stock_code}");

        public async void ReadCSVCoupled(string stock_code)
        {
            Dictionary<string, string> stockData = new Dictionary<string, string>();
            string path = $@"https://stooq.com/q/l/?s={stock_code.ToLower()}&f=sd2t2ohlcv&h&e=csv";
            var response = new System.Net.WebClient().DownloadString(path).Split("\r\n");
            var headers = response[0];
            var headersValues = headers.Split(',');
            var data = response[1];
            var dataValues = data.Split(',');
            for (int i = 0; i < headersValues.Count() - 1; i++)
                stockData.Add(headersValues[i], dataValues[i]);
            await Clients.All.SendAsync("ReceiveMessage", string.Empty, $"{stock_code.ToUpper()} quote is ${stockData.FirstOrDefault(k => k.Key == "Close").Value} per share");
        }
    }
}
