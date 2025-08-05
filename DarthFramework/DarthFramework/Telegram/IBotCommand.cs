using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthFramework.Telegram
{
    public interface IBotCommand
    {
        public string Name { get; }

        public Task<ProcessingResult> Process(ReceivedMessage message);
    }
}
