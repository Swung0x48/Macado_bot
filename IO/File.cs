using System;
using System.Text;
using System.Threading.Tasks;
using Macado_bot.Utils;
using Newtonsoft.Json;

namespace Macado_bot.IO
{
    public class File
    {
        public static async Task WriteConf(string filename, object obj)
        {
            try
            {
                await System.IO.File.WriteAllTextAsync(
                    filename, 
                    JsonConvert.SerializeObject(obj, Formatting.Indented)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task<T> ReadConf<T>(string filename, bool isAutoSave = false)
        {
            if (isAutoSave)
            {
                Console.WriteLine("Auto saving configuration file...");
            }
            return JsonConvert.DeserializeObject<T>(
                await System.IO.File.ReadAllTextAsync(
                    filename, 
                    Encoding.UTF8
                    )
                );
        }
    }
}