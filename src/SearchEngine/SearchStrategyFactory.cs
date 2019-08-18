using Shared;
using System;

namespace SearchEngine
{
    public class SearchStrategyFactory
    {
        IPageLoader loader;
        public SearchStrategyFactory(IPageLoader loader)
        {
            this.loader = loader;
        }

        public ISearchStrategy Get(string engine)
        {
            Guard.ListNotNullOrEmpty(engine, nameof(engine));

            switch (engine.ToLower())
            {
                case "google":
                {
                    return new GoogleSearchStrategy(loader);
                }
                case "bing":
                {
                    return new BingSearchStrategy(loader);
                }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
