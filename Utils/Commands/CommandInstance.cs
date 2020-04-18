namespace Macado_bot.Utils.Commands
{
    public class CommandInstance
    {
        public static readonly CommandRouter CommandManager = new CommandRouter();
        static CommandInstance()
        {
            CommandManager.RegisterCommand(new Implemented.InfoCommand());
        }
    }
}