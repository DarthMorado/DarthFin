using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthFramework.Telegram
{
    public class Answer
    {
        public string? Text { get; set; }
        //public Stream? Image { get; set; }
        public long ChatId { get; set; }
        public bool DisableWebPagePreview { get; set; } = true;
        public bool IsHtml { get; set; } = false;
        public int? ReplyTo { get; set; }
        public int? MessageId { get; set; }
        public AnswerType Type { get; set; }

        public enum AnswerType
        {
            New,
            Edit,
            Delete
        }
    }
}
