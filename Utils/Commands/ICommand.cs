using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Macado_bot.Utils.Commands
{
    public interface ICommand
    {
        string CmdLiteral { get; }
        string Argv { get; }
        int AccessLevelReq { get; }    // 0 means owner only. 1 means open to all users.
        Task<bool> ExecuteAsync(
            TelegramBotClient botClient, 
            Update update
            );
    }
}