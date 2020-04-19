using System.Threading.Tasks;
using Macado_bot.IO;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Macado_bot.Utils.Commands.Implemented
{
    public class GetLatestCommand : ICommand
    {
        public string CmdLiteral => "getlastest";
        public string Argv { get; }

        public async Task<bool> ExecuteAsync(TelegramBotClient botClient, Update update)
        {
            ChatId chatId = update.Message.From.Id;
            var vlist = await Bilibili.GetSpacePage(1, 1);

            foreach (var v in vlist)
            {
                //Console.WriteLine(t.ToString());
                await Bot.BotClient.SendTextMessageAsync(
                    chatId,
                    $"{v["author"]}的最新视频：\n" +
                    $"{v["title"]}\n" +
                    $"https://www.bilibili.com/video/{v["bvid"]}"
                );

            }

            return true;
        }
    }
}