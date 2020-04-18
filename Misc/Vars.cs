using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using Macado_bot.Utils.Commands;
using Telegram.Bot;

namespace Macado_bot.Misc
{
    public static class Vars {
        public static Stopwatch Stopwatch = new Stopwatch();
        public readonly static string AppExecutable = 
            Assembly.GetExecutingAssembly().Location;
        public readonly static string AppDirectory = 
            (new FileInfo(AppExecutable)).DirectoryName;
        public static string ConfFile = 
            Path.Combine(AppDirectory, "Macado_bot.json");
        public static string LangFile = 
            Path.Combine(AppDirectory, "Macado_bot_locale.json");

        public static ConfObj CurrentConf = new ConfObj();
        
        
        public static string Uptime
        {
            get => Stopwatch.Elapsed.ToString();
        }
    }
}