using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Macado_bot.Utils.Commands
{
    public interface ICommand
    {
        string CmdLiteral { get; }
        string Argv { get; }
        Task<bool> ExecuteAsync(
            TelegramBotClient botClient, 
            Update update
            );
    }
}