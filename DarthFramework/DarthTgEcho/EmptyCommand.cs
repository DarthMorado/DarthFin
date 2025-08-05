using DarthFramework.Telegram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthTgEcho
{
    public class EmptyCommand : IBotCommand
    {
        public string Name => string.Empty;

        public async Task<ProcessingResult> Process(ReceivedMessage message)
        {
            return new ProcessingResult()
            {
                Answers = new List<Answer>()
                {
                    new Answer()
                    {
                        ChatId = message.ChatId,
                        MessageId = message.MessageId,
                        Text = message.Text,
                    }
                }
            };
        }
    }
}