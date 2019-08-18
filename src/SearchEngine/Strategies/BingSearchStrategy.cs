using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class BingSearchStrategy : SearchStrategyBase, ISearchStrategy
    {           
        public BingSearchStrategy(IPageLoader loader): base(loader)
        {   
            BaseUrl = "http://bing.com.au";
        }

        protected override string getBaseUrl(string text, int numberResults)
        {
            return $"{BaseUrl}/search?q={text}&count={numberResults}";
        }
        protected override async Task<List<string>> parseLinks(string pageContent)
        {              
            var r = new Regex(@"<(a|link).*?href=(""|')(.+?)(""|').*?>");

            var links = r.Matches(pageContent ?? string.Empty)
                         .Select(m => m.Value);

            return links.ToList();
        }        
    }
}
