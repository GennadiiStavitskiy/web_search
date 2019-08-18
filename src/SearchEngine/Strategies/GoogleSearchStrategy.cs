using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class GoogleSearchStrategy: SearchStrategyBase, ISearchStrategy
    {   
        public GoogleSearchStrategy(IPageLoader loader): base(loader)
        {
           BaseUrl = "http://google.com.au";
        }

        protected override string getBaseUrl(string text, int numberResults)
        {
            return $"{BaseUrl}/search?q={text}&count={numberResults}";
        }

        protected override async Task<List<string>> parseLinks(string pageContent)
        {              
            var r = new Regex(@"<(a|link).*?href=(""|')(.+?)(""|').*?>");

            var links = r.Matches(pageContent ?? string.Empty)
                         .Where(m => m.Value.StartsWith(@"<a href=""/url?q="))
                         .Select(m => m.Value.Remove(0, @"<a href=""/url?q=".Length))
                         .Select(s => s.Substring(0, s.Length - @""">".Length))
                         .ToList();

            return links.ToList();
        }        
    }
}
