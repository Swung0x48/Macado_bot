using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Macado_bot.IO
{
    public class BilibiliStat
    {
        public int PlayCount = 0;
        public int CoinCount = 0;
        public int FavCount = 0;
        public int ShareCount = 0;

        public async Task<BilibiliStat> Create()
        {
            return new BilibiliStat();
        }
        private BilibiliStat()
        {
            
        }
    }
    
    public class Bilibili
    {
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

        public static async Task<BilibiliStat> GetBilibiliStat(string uperUid)
        {
            throw new NotImplementedException(); // TODO : Implement this.
        }
    }
}