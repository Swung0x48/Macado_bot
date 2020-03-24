using System;
using System.Threading;
using System.Threading.Tasks;
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
                    BotClient.SendTextMessageAsync(e.Message.Chat, $"Current uptime is {Vars.Uptime}");
                }
                else if (e.Message.Text == "/start")
                {
                    BotClient.SendTextMessageAsync(e.Message.Chat, " ");
                }
                else if (e.Message.Text == "/settings")
                {
                    
                }
                else if (e.Message.Text == "/info" || e.Message.Text == "/info@Macado_bot")
                {
                    try
                    {
                        string raw = await IO.Networking.MakeHttpRequestAsync("http://api.bilibili.com/x/relation/stat?vmid=490751924");
                        
                        Newtonsoft.Json.Linq.JObject jsonObj = Newtonsoft.Json.Linq.JObject.Parse(raw);
                        string target = jsonObj["data"]["follower"].ToString();
                        //var userDataObj = await Json.Parse<UserDataObj>(rawUserObj.message);
                        //BotClient.SendTextMessageAsync(e.Message.Chat, raw);
                        BotClient.SendTextMessageAsync(e.Message.Chat, $"当前玛卡豆关注数：{target}");

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                    
                }
            }
        }
    }
}