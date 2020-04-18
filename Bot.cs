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
            // TODO : delete above
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

        //static async void OnMessage(object sender, MessageEventArgs e) // TODO : delete this
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
            
            
            
            // if (e.Message.Text != null)    // TODO : delete this
            // {
            //     
            //     Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.\n");
            //     if (e.Message.Text == "/uptime")                                            // TODO : Implement CmdProcessor to replace if blocks
            //     {
            //         await BotClient.SendTextMessageAsync(e.Message.Chat, $"Current uptime is {Vars.Uptime}");
            //     }
            //     else if (e.Message.Text == "/start")
            //     {
            //         await BotClient.SendTextMessageAsync(e.Message.Chat, " ");
            //     }
            //     else if (e.Message.Text == "/settings")
            //     {
            //         
            //     }
            //     else if (e.Message.Text == "/info" || e.Message.Text == $"/info@{botInstance.Username}")
            //     {
            //         await Networking.GetUpInfo(e.Message.Chat);
            //
            //     }
            //     else if (e.Message.Text == "/getlatest" || e.Message.Text == $"/getlatest@{botInstance.Username}")
            //     {
            //         await Networking.GetLatestVidInfo(e.Message.Chat);
            //     }
            //     
            // }
        }

        private static async Task UserLogic(Update update)
        {
            if (await CommandInstance.CommandManager.Execute(Bot.BotClient, update))
                return;
        }

        private static async Task OwnerLogic(Update update)
        {
            throw new NotImplementedException();            // TODO : Implement this.
        }
    }
}