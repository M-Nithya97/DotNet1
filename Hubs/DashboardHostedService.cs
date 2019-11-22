using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class DashboardHostedService: IHostedService
    {
        private Timer _timer;
       
        private readonly IHubContext<NotificationHub> _hubContext;

        public DashboardHostedService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }
        private string getRandomString()

        {

            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, random.Next(10, 16))

              .Select(s => s[random.Next(s.Length)]).ToArray());

        }
        private void DoWork(object state)
        {
            _hubContext.Clients.All.SendAsync("SendMessage",
                new
                {
                    val1 =   getRandomString(),
                    val2 = getRandomString(),
                    val3 = getRandomString(),
                    val4 = getRandomString()
                });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
