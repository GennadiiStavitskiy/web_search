using SearchEngine.Models;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine
{
    public abstract class SearchStrategyBase 
    {        
        private IPageLoader loader;

        protected string BaseUrl { get; set; }


        public SearchStrategyBase(IPageLoader loader)
        {
            this.loader = loader;            
        }

        protected abstract string getBaseUrl(string text, int numberResults);
        protected abstract Task<List<string>> parseLinks(string pageContent);

        public async Task<LoadResult> SearchAsync(string url, string text, int numberResults)
        {
            Guard.ListNotNullOrEmpty(url, nameof(url));
            Guard.ListNotNullOrEmpty(text, nameof(text));
            Guard.IntMoreThanZero(numberResults, nameof(numberResults));

            var loadUrl = getBaseUrl(text, numberResults);

            var loadResult = await loader.LoadAsync(loadUrl);

            if(loadResult.IsSuccess)
            {
                var links = await parseLinks(loadResult.ContentStr);

                var matches = new List<int>();

                for (int i = 0; i < links.Take(numberResults).Count(); i++)
                {
                    var link = links[i];
                    if(link.Contains(url))
                    {
                        matches.Add(i+1);
                    }
                }

                if(!matches.Any())
                {
                    matches.Add(0);
                }
               
                loadResult.ContentStr = string.Join(',', matches);
            }
            else
            {
                loadResult.ContentStr = string.Empty;
            }

            return loadResult;
        }            
    }
}
