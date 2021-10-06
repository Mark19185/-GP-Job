using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GP_Job.Models
{
    class Article
    {
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string FullText { get; set; } = "";
        public string PreviewImageLink { get; set; }
        public string Source { get; set; }
        public string Date { get; set; }
        public string Link { get;set; }
        public override string ToString()
        {
            return $"{Title}\n{PreviewImageLink}\n{ShortText}\n{Source}";
        }
    }
}
