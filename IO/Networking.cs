using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;

namespace Macado_bot.IO
{
    public class Networking
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        public static async Task<string> MakeHttpRequestAsync(string url)
        {
            try	
            {
                string responseBody = await HttpClient.GetStringAsync(url);
                return responseBody;
            }  
            catch(HttpRequestException e) 
            {
                return $"Error :{e.Message} ";
            } 
        }
    }
}