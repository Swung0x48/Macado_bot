using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Macado_bot.Misc;

namespace Macado_bot
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Vars.Stopwatch.Start();                                                // Start stopwatch to record uptime.

            if (!File.Exists(Vars.ConfFile))                                       // Check whether config file exists.
            {
                Console.WriteLine(Language.MsgConfNotFound);
                Console.WriteLine(Language.MsgFirstBoot);
                Console.WriteLine(Language.MsgReqApiKey);
                Vars.CurrentConf.Apikey = Console.ReadLine();                       // If not, request API key.
                await IO.File.WriteConf(Vars.ConfFile, Vars.CurrentConf);           // Save conf to file.
            }
            
            Console.WriteLine(Language.MsgReadConf);                                
            Vars.CurrentConf = await IO.File.ReadConf<ConfObj>(Vars.ConfFile);      // Read from conf file.
            
            
            await IO.File.WriteConf(Vars.ConfFile, Vars.CurrentConf);
            
            Console.WriteLine(Language.MsgBotInit);
            await Bot.Init(Vars.CurrentConf.Apikey);                               // Initialize bot.
            
            
        }
    }
}