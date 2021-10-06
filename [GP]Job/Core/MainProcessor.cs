using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _GP_Job.Core
{
    class MainProcessor
    {
        public async static void GrabData()
        {
            List<PostParser.Response> sources = new List<PostParser.Response>();
            sources.AddRange( new PostParser.Response[] {

                new PostParser.VGTimes("https://vgtimes.ru/articles/"),
                new PostParser.VGTimes("https://vgtimes.ru/tags/Игровые+новости/"),
                new PostParser.VGTimes("https://vgtimes.ru/tags/Железо+и+технологии/")
                
            });
            foreach (PostParser.Response source in sources)
            {
                source.SetNotify = ProcessPreparedArticles;
            }

            await  Task.Run(() => { 
                while (true)
                {
                    foreach (PostParser.Response source in sources)
                    {
                        source.ProcessedData();
                    }
                    Thread.Sleep(900000);
                }
            });
        }
        protected static async void ProcessPreparedArticles(Array pack, int sourceID)
        {
            Console.WriteLine($"[{DateTime.Now}] Получен пакет записей\n");
            foreach (Models.Article article in pack)
            { 
                await SQL.InsertPost(article, sourceID);
            }
        }
    }
}
