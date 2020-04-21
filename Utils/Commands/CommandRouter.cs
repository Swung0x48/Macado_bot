using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macado_bot.Misc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Macado_bot.Utils.Commands
{
    public class CommandRouter
    {
        // public ICommand this[string prefix] => commands.FirstOrDefault(command => command.CmdLiteral == prefix);
        private readonly List<ICommand> _cmdStorages = new List<ICommand>();

        public void RegisterCommand(ICommand command)
        {
            if (_cmdStorages.Any(x => x.CmdLiteral == command.CmdLiteral))
            { throw new ArgumentException($"A commandline with literal value \"/{command.CmdLiteral}\" already exists.", nameof(command)); }

            _cmdStorages.Add(command);
        }
        
        
        
        public async Task<bool> Execute(TelegramBotClient botClient, Update update)
        {
            var hasGrantedAccess = update.Message.From.Id == Vars.CurrentConf.OwnerUID;  // TODO : Implement permission system
            
            if (!Preprocessor.IsCommand(update.Message.Text)) return false;
            
            var incomingCmd = new Preprocessor(update.Message.Text);
            foreach (var i in _cmdStorages)
            {
                if (incomingCmd.GetCommand() == i.CmdLiteral)
                {
                    try
                    {
                        _ = await i.ExecuteAsync(botClient, update).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception while executing commandline: {ex}");
                        Environment.Exit(1);
                    }
                }
            }

            return true;
        }

        

    }
}