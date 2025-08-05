using DarthFramework.Telegram;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthTgEcho
{
    public interface IEchoBot : ICoreBot { }

    public class EchoBot : CoreBot, IEchoBot
    {
        public EchoBot(IOptions<EchoBotOptions> options) : base(options)
        {
        }

        public override List<IBotCommand> Commands 
        { 
            get => new List<IBotCommand>()
            {
                new EmptyCommand()
            }; 
        }
    }
}
