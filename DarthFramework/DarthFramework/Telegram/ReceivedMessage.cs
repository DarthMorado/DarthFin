using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DarthFramework.Telegram
{
    public class ReceivedMessage
    {
        public string? Text { get; set; }
        //public object? Image { get; set; }
        //public bool IsCallback { get; set; }
        //public bool PrivateChat { get; set; }
        public long ChatId { get; set; }
        //public long SenderId { get; set; }
        public int MessageId { get; set; }
        //public int? ReplyTo { get; set; }
        //public string? ReplyToText { get; set; }
        //public bool ReplyToBot { get; set; }
        public string? Command { get; set; }
        public string? Parameter { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();

        public void Normalise()
        {
            Text = Text?.Trim();

            if (Text != null
                && Text.StartsWith("/"))
            {
                var parts = Text
                            .Substring(1)
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .ToList();
                if (parts.Any())
                {
                    Command = parts
                                .First()
                                .ToLower();
                    if (parts.Count > 1)
                    {
                        Parameter = Text.Substring(Command.Length + 1).Trim();
                        Parameters = parts
                                        .Skip(1)
                                        .ToList();
                    }
                }
            }
            if (Command is null)
            {
                Command = String.Empty;
            }
        }

        public ReceivedMessage(ITelegramBotClient botClient, Message message)
        {
            Text = message.Text;
            ChatId = message.Chat.Id;
            MessageId = message.MessageId;
        }
    }
}
