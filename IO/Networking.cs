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
public static async Task GetUpInfo(ChatId chatId, string uid = "490751924")
        {
            try
            {
                string rawFollower = await MakeHttpRequestAsync($"http://api.bilibili.com/x/relation/stat?vmid={uid}");
                        
                JObject jsonFollowerObj = JObject.Parse(rawFollower);
                string strFollower = jsonFollowerObj["data"]["follower"].ToString();

                string rawUpStat =
                    await MakeHttpRequestAsync(
                        $"https://api.bilibili.com/x/space/upstat?mid={uid}&jsonp=jsonp");
                JObject rawUpStatObj = JObject.Parse(rawUpStat);
                // string strView = jsonSpaceObj["data"]["archive"]["view"].ToString();
                string strLikes = rawUpStatObj["data"]["likes"].ToString();
                
                // TODO : Refactor viewer counter.
           ////////////////////////////////    Viewer counter. To be refactored.    ////////////////////////////
                
            var playCount = 0;
            var coinCount = 0;
            var favCount = 0;
            var shareCount = 0;

            var mid = 490751924;
            var pageSize = 20;
            var pageNo = 1;
            
            Console.WriteLine($"Init: {Vars.Stopwatch.Elapsed.ToString()}");
            for (; ; pageNo ++)
            {
                var vlist = await GetSpacePage(100, pageNo);
                
                if (vlist.Count == 0) break;
                
                foreach (var t in vlist)
                {
                    try
                    {
                        playCount += int.Parse(t["play"].ToString());            // play counter
                        string bvid = t["bvid"].ToString();
                        try
                        {
                            string rawVideo = await Networking.MakeHttpRequestAsync(
                                $"https://api.bilibili.com/x/web-interface/view?bvid={bvid}");
                            JObject jsonVideoObj = JObject.Parse(rawVideo);
                            // Console.WriteLine(jsonVideoObj["data"]["stat"]["coin"].ToString());
                            coinCount += int.Parse(jsonVideoObj["data"]["stat"]["coin"].ToString()); // coin counter
                            favCount += int.Parse(jsonVideoObj["data"]["stat"]["favorite"].ToString());    // favourite counter.
                            shareCount += int.Parse(jsonVideoObj["data"]["stat"]["share"].ToString()); // share counter.
                        }
                        catch (HttpRequestException e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }


            //Console.WriteLine(videoArray.ToString());
            Console.WriteLine($"pageNo: {pageNo}");
            Console.WriteLine($"playCount: {playCount}");
            Console.WriteLine($"coinCount: {coinCount}");
            Console.WriteLine($"favCount: {favCount}");
            Console.WriteLine($"shareCount: {shareCount}");
            Console.WriteLine();
            Console.WriteLine($"After crawling: {Vars.Stopwatch.Elapsed.ToString()}");
            Console.WriteLine();
            /////////////////////////////////////////    End of viewer counter     /////////////////////////////////////


                //Console.WriteLine(videoArray.ToString());
                // Console.WriteLine(playCount);
                
                await Bot.BotClient.SendTextMessageAsync(chatId, $"👀 关注： {strFollower}\n" +
                                                               $"▶️ 播放： {playCount}\n" +
                                                               $"👍 点赞： {strLikes}\n" +
                                                               $"💰 投币： {coinCount}\n" +
                                                               $"🌟 收藏： {favCount}\n" +
                                                               $"↪️ 转发： {shareCount}\n");
                
                        

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
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