using System.Threading.Tasks;
using Macado_bot.IO;
using Macado_bot.Misc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Macado_bot.Utils.Commands.Implemented
{
    public class UptimeCommand : ICommand
    {
        public string CmdLiteral => "uptime";
        public string Argv { get; }
        public int AccessLevelReq => 1;

        public async Task<bool> ExecuteAsync(TelegramBotClient botClient, Update update)
        {
            await Bot.BotClient.SendTextMessageAsync(
                update.Message.Chat, 
                $"Current uptime is {Vars.Uptime}"
                );
            
            return true;
        }
    }
}