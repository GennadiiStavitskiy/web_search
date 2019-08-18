using Moq;
using SearchEngine;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineTest
{
    public class SearchStrategyFactoryTests
    {
        SearchStrategyFactory factory;

        public SearchStrategyFactoryTests()
        {
            var loader = Mock.Of<IPageLoader>();
            factory = new SearchStrategyFactory(loader);
        }

        [Fact]
        public async Task it_should_return_google_search_strategy_when_engine_is_google()
        {
            var strategy = factory.Get("google");

            Assert.NotNull(strategy);
            Assert.IsType<GoogleSearchStrategy>(strategy);
        }

        [Fact]
        public async Task it_should_return_throw_ArgumentNullException_when_engine_is_null()
        {           
            Assert.Throws<ArgumentNullException>(() => factory.Get(null));
        }

        [Fact]
        public async Task it_should_return_throw_NotImplementedException_when_engine_not_found()
        {
            Assert.Throws<NotImplementedException>(() => factory.Get("NonExistingEngine"));
        }
    }
}
