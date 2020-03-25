using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Macado_bot.Misc;
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

        public static async Task GetInfo(ChatId chatId, string uid = "490751924")
        {
            try
            {
                string rawFollower = await IO.Networking.MakeHttpRequestAsync("http://api.bilibili.com/x/relation/stat?vmid=490751924");
                        
                Newtonsoft.Json.Linq.JObject jsonFollowerObj = Newtonsoft.Json.Linq.JObject.Parse(rawFollower);
                string strFollower = jsonFollowerObj["data"]["follower"].ToString();

                string rawSpace =
                    await IO.Networking.MakeHttpRequestAsync(
                        $"https://api.bilibili.com/x/space/upstat?mid={uid}&jsonp=jsonp");
                Newtonsoft.Json.Linq.JObject jsonSpaceObj = Newtonsoft.Json.Linq.JObject.Parse(rawSpace);
                // string strView = jsonSpaceObj["data"]["archive"]["view"].ToString();
                string strLikes = jsonSpaceObj["data"]["likes"].ToString();
                
                // TODO : Refactor viewer counter.
                ////////////////////////////    Viewer counter. To be refactored.    ////////////////////////////
                int playCount = 0;

                int pageSize = 100;
                for (int pageNo = 1; ; pageNo ++)
                {
                    string rawVideo = await Networking.MakeHttpRequestAsync(
                        $"https://api.bilibili.com/x/space/arc/search?mid=490751924&" +
                        $"ps={pageSize}&" +
                        $"tid=0&" +
                        $"pn={pageNo}&" +
                        $"keyword=&order=pubdate&jsonp=jsonp");
                    // Console.WriteLine(rawVideo);
                    JObject jsonVideoObj = JObject.Parse(rawVideo);
                    IList<JToken> videoArray = jsonVideoObj["data"]["list"]["vlist"].Children().ToList();
                
                    if (videoArray.Count == 0) break;
                
                    for (int j = 0; j < videoArray.Count; j ++)
                    {
                        try
                        {
                            playCount += int.Parse(videoArray[j]["play"].ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
                ////////////////////////////    End of Viewer counter.    ////////////////////////////


                //Console.WriteLine(videoArray.ToString());
                // Console.WriteLine(playCount);
                
                await Bot.BotClient.SendTextMessageAsync(chatId, $"👀 {strFollower}\n" +
                                                               $"▶️ {playCount}\n" +
                                                               $"👍 {strLikes}");
                
                        

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }


    }
}