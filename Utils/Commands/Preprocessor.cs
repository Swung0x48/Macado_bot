using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Macado_bot.Utils.Commands
{
    public class Preprocessor
    {
        private static readonly Regex CmdRegex = new Regex("^/([^@\n]+)(@.+)?");
        private string command;
        private string calledUserName;
        private List<string> args;
        
        public Preprocessor(string rawMsg)
        {
            if (!IsCommand(rawMsg)) return;
            
            string cmd = rawMsg.Split(' ').First();
            args = rawMsg.Split(' ').ToList();

            command = CmdRegex.Match(cmd).Groups[1].Value;
            calledUserName = CmdRegex.Match(cmd).Groups[2].Value;
            
            args.Remove(args[0]);
        }
        
        public static bool IsCommand(string rawMsg)
        {
            if (CmdRegex.Match(rawMsg).Groups[0].Value != "") 
                return true;
            
            return false;
        }
        public string GetCommand()
            => command;
        
        public string GetCalledUser()
            => calledUserName;
    }
}