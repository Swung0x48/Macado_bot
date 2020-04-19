using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Args;
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


        public static async Task<IList<JToken>> GetSpacePage(int pageSize, int pageNo, string mid = "490751924")
        {
            var rawSpace = await Networking.MakeHttpRequestAsync(
                $"https://api.bilibili.com/x/space/arc/search?mid={mid}&" +
                $"ps={pageSize}&" +
                $"tid=0&" +
                $"pn={pageNo}&" +
                $"keyword=&order=pubdate&jsonp=jsonp");
            //Console.WriteLine(rawVideo);
            var jsonSpaceObj = JObject.Parse(rawSpace);
            return jsonSpaceObj["data"]["list"]["vlist"].Children().ToList();
        }

        public static async Task GetLatestVidInfo(ChatId chatId, string uid = "490751924")
        {
            var vlist = await GetSpacePage(1, 1);

            foreach (var v in vlist)
            {
                //Console.WriteLine(t.ToString());
                await Bot.BotClient.SendTextMessageAsync(chatId,$"{v["author"]}的最新视频：\n" +
                                                                $"{v["title"]}\n" +
                                                                $"https://www.bilibili.com/video/{v["bvid"]}");
            }
        }


    }
}