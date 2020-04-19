using Macado_bot.Utils.Commands.Implemented;

namespace Macado_bot.Utils.Commands
{
    public class CommandInstance
    {
        public static readonly CommandRouter CommandManager = new CommandRouter();
        static CommandInstance()
        {
            CommandManager.RegisterCommand(new InfoCommand());
            CommandManager.RegisterCommand(new GetLatestCommand());
            CommandManager.RegisterCommand(new UptimeCommand());
        }
    }
}