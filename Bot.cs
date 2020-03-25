using System;
using System.Threading;
using System.Threading.Tasks;
using Macado_bot.IO;
using Macado_bot.Utils;
using Macado_bot.Misc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Macado_bot
{
    public class Bot
    {
        public static TelegramBotClient BotClient;

        public static async Task Init(string apiKey)
        {
            BotClient = new TelegramBotClient(apiKey); // Get the API Key to start bot.
            var botInstance = BotClient.GetMeAsync().Result; // Get the bot instance info.
            Console.WriteLine($"ID: {botInstance.Id} \nName: {botInstance.FirstName}."); // Survival confirmation.
            BotClient.OnMessage += OnMessage;                                        // Add OnMessage() to bot Client's OnMessage listener
            BotClient.StartReceiving();

            while (true)                                                                    
            {
                Thread.Sleep(int.MaxValue);                                // Keeping the whole thing running.
            }

        }

        static async void OnMessage(object sender, MessageEventArgs e) 
        {
            if (e.Message.Text != null)
            {
                
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                if (e.Message.Text == "/uptime")                                            // TODO : Implement CmdProcessor to replace if blocks
                {
                    await BotClient.SendTextMessageAsync(e.Message.Chat, $"Current uptime is {Vars.Uptime}");
                }
                else if (e.Message.Text == "/start")
                {
                    await BotClient.SendTextMessageAsync(e.Message.Chat, " ");
                }
                else if (e.Message.Text == "/settings")
                {
                    
                }
                else if (e.Message.Text == "/info" || e.Message.Text == "/info@Macado_bot")
                {
                    await Networking.GetInfo(e.Message.Chat);

                }
            }
        }
    }
}