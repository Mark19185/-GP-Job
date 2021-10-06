using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using System.Net;
using System.Drawing;
using Leaf.xNet;

namespace _GP_Job.Core
{
    class PostParser
    {
        #region базовый класс статьи
        public abstract class Response
        {
            public delegate void Notificator(Array pack, int SourceID);
            public Notificator _Notify;
            public string BasePath; //Домен
            public string TargetPath; //ссылка до страницы, откуда получаем инф
            public abstract string[] ProcessedData(); //возвращает массив с вёрсткой элементов

            public Response()
            {
            }
            public Response(string path, string domain)
            {
                this.BasePath = domain;
                TargetPath = path;
            }
            public Notificator SetNotify
            {
                get
                {
                    return _Notify;
                }
                set
                {
                    _Notify = value;
                }
            }
        }
        #endregion
        public class VGTimes : Response
        {
            public event Notificator Notify;
           
            public VGTimes(string path) : base(path, "https://vgtimes.ru")
            {
            }
            public override string[] ProcessedData()
            {
                using (var request = new HttpRequest())
                {
                    string _response = request.Get(TargetPath).ToString();
                    HtmlDocument page = new HtmlDocument();
                    page.LoadHtml(_response);

                    var articles = page?.DocumentNode.QuerySelectorAll(".item-main").ToArray();
                    List<Models.Article> _articles = new List<Models.Article>();
                    foreach (var arcticle in articles)
                    {
                        HtmlDocument section = new HtmlDocument();
                        section.LoadHtml(arcticle.OuterHtml);
                        string previewPicture = "";
                        if (section?.DocumentNode.QuerySelectorAll("img").ToArray().Count() > 0)
                        {
                            previewPicture = $"{BasePath}{section?.DocumentNode.QuerySelectorAll("img")?.ToArray()[0].OuterHtml.Split('"')[5]}";
                        }
                        _articles.Add(new Models.Article()
                        {
                            Title = section?.DocumentNode.QuerySelectorAll("span").ToArray()[0].InnerText,
                            PreviewImageLink = previewPicture,
                            ShortText = section?.DocumentNode.QuerySelectorAll(".v12").ToArray()[0].InnerText,
                            Source = this.BasePath.Split('/')[2],
                            Date = section?.DocumentNode.QuerySelectorAll(".news_item_time").ToArray()[0].OuterHtml.Split('"')[2].Remove(0, 1).Replace("</span>", ""),
                            Link = section?.DocumentNode.QuerySelectorAll("a").ToArray()[0].OuterHtml.Split('"')[1]
                        }); ; ;
                    }
                    _Notify(_articles.ToArray(), 1);
                }
                return new string[] { };
            }
        }
    }
}
