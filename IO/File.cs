using System;
using System.Text;
using System.Threading.Tasks;
using Macado_bot.Utils;

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
                    await Json.Serialize(obj)
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
            return await Json.Parse<T>(
                await System.IO.File.ReadAllTextAsync(
                    filename, 
                    Encoding.UTF8
                    )
                );
        }
    }
}