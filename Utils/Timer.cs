using System;
using System.Threading;
using System.Threading.Tasks;
using Macado_bot.IO;
using Telegram.Bot.Types;

namespace Macado_bot.Utils
{
    public class Timer
    {
        
        public static async void ScheduledTask(TimeSpan timeToExecute)
        {
            await Task.Delay((int)timeToExecute.Subtract(DateTime.Now.TimeOfDay).TotalMilliseconds);
            ChatId chatId;
            //chatId = -1001303334160;
            chatId = 288763391;
            await Networking.GetUpInfo(chatId);
        }
        
        
    }

}