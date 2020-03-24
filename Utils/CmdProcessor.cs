using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Macado_bot.Utils
{
    public class CmdProcessor
    {
        public static async Task<bool> CmdPreprocess(string input)
        {
            string Pattern = $"/()@";

            Regex rgx = new Regex(Pattern);
            
            return true;
        }
    }
}