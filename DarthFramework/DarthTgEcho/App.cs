using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthTgEcho
{
    public interface IApp
    {

    }

    public class App : BackgroundService
    {
        private readonly IEchoBot _bot;

        public App(IEchoBot bot)
        {
            _bot = bot;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _bot.Start();
            while(!cancellationToken.IsCancellationRequested)
            {
                //app
            }
            _bot.Stop();
        }
    }
}
