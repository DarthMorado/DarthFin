using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Options;

namespace DarthFramework.Telegram
{
    public interface ICoreBot
    {
        public void Start();
        public void Stop();
    }

    public class CoreBot : ICoreBot
    {
        public virtual List<IBotCommand> Commands { get; set; } = new List<IBotCommand>();

        private readonly TelegramBotClient Bot;
        private CancellationTokenSource _cancellationToken;
        private ReceiverOptions _receiverOptions;

        public CoreBot(IOptions<BotOptions> options)
        {
            Bot = new TelegramBotClient(options.Value.ApiKey);
            var botUser = Bot.GetMe().Result;
            _cancellationToken = new CancellationTokenSource();
            _receiverOptions = new() { AllowedUpdates = { } };
        }

        public void Start()
        {
            Bot.StartReceiving(ProcessUpdate,
                          ProcessError,
                          _receiverOptions,
                          _cancellationToken.Token);
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
        }

        public async Task ProcessUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                ReceivedMessage? message = update.Type switch
                {
                    UpdateType.Message => new ReceivedMessage(botClient, update.Message!),
                    _ => null
                };

                if (message is null) return;

                message.Normalise();

                foreach (IBotCommand command in Commands)
                {
                    try
                    {

                        if (command.Name.Equals(message.Command, StringComparison.OrdinalIgnoreCase))
                        {
                            var result = await command.Process(message);
                            await FinaliseResult(result);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch
            {
            }
        }

        public async Task ProcessError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception);
        }

        public async Task FinaliseResult(ProcessingResult result)
        {
            foreach (var answer in result.Answers)
            {
                if (answer.Type == Answer.AnswerType.New)
                {
                    if (!String.IsNullOrWhiteSpace(answer.Text))
                    {
                        await Bot.SendMessage(chatId: answer.ChatId,
                                                       text: answer.Text.ToString(CultureInfo.CreateSpecificCulture("en-GB")),
                                                       replyParameters: (answer.ReplyTo == null ? null : new ReplyParameters() {MessageId = answer.ReplyTo.Value }),
                                                       parseMode: answer.IsHtml ? ParseMode.Html : ParseMode.MarkdownV2,
                                                       linkPreviewOptions : new LinkPreviewOptions() { IsDisabled = answer.DisableWebPagePreview },
                                                       cancellationToken: new CancellationTokenSource(1000).Token
                                                      );
                    }
                }
                else if (answer.Type == Answer.AnswerType.Edit && answer.MessageId.HasValue)
                {
                    if (!String.IsNullOrWhiteSpace(answer.Text))
                    {
                        await Bot.EditMessageText(chatId: answer.ChatId,
                            text: answer.Text,
                            messageId: answer.MessageId.Value,
                            parseMode: answer.IsHtml ? ParseMode.Html : ParseMode.MarkdownV2,
                            linkPreviewOptions: new LinkPreviewOptions() { IsDisabled = answer.DisableWebPagePreview }
                        );
                    }
                }
                else if (answer.Type == Answer.AnswerType.Delete && answer.MessageId.HasValue)
                {
                    await Bot.DeleteMessage(chatId: answer.ChatId,
                        messageId: answer.MessageId.Value
                        );
                }
            }
        }
    }
}
