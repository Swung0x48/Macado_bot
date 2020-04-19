using System;
using System.Threading;
using System.Threading.Tasks;
using Macado_bot.IO;
using Macado_bot.Misc;
using Macado_bot.Utils.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Macado_bot
{
    public class Bot
    {
        public static TelegramBotClient BotClient;

        public static async Task Init(string apiKey, CancellationTokenSource cts)
        {
            BotClient = new TelegramBotClient(apiKey); // Get the API Key to start bot.
            var botInstance = BotClient.GetMeAsync().Result; // Get the bot instance info.
            Console.WriteLine($"ID: {botInstance.Id} \nName: {botInstance.FirstName}."); // Survival confirmation.
            BotClient.OnUpdate += OnUpdate;                                        // Add OnMessage() to bot Client's OnMessage listener
            BotClient.StartReceiving(
                new[] { UpdateType.Message },
                    cts.Token
                    );

            while (true)                                                                    
            {
                //Thread.Sleep(int.MaxValue);
                Console.ReadKey();                                                     // Keeping the whole thing running.

            }

        }

        static async void OnUpdate(object sender, UpdateEventArgs e) 
        {
            var botInstance = BotClient.GetMeAsync().Result; // Get the bot instance info.
            var update = e.Update;
            if (update.Type != UpdateType.Message) return;
            if (update.Message.From.IsBot) return;
            // if (update.Message.Chat.Type != ChatType.Private) return;

            if (update.Message.From.Id == Vars.CurrentConf.OwnerUID)
            {
                await OwnerLogic(update).ConfigureAwait(false);
            }
            else
            {
                await UserLogic(update).ConfigureAwait(false);
            }
        }

        private static async Task UserLogic(Update update)
        {
            if (await CommandInstance.CommandManager.Execute(Bot.BotClient, update))
                return;
        }

        private static async Task OwnerLogic(Update update)
        {
            throw new NotImplementedException();            // TODO : Route /uptime command here.
        }
    }
}