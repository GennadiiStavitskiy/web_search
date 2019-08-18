using Moq;
using SearchEngine;
using SearchEngine.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SearchEngineTest
{
    public class GoogleSearchStrategyTest
    {
        ISearchStrategy strategy;
        IPageLoader loader;
        string contentStr;

        public GoogleSearchStrategyTest()
        {
            loader = Mock.Of<IPageLoader>();
            strategy = new GoogleSearchStrategy(loader);

            contentStr = @"<a href=""/url?q=http://test.comtest.com?q=dddddd&amp;ie=UTF-8&amp;ei=VbVSXbSfNYGT8AOhgZfIBQ&amp;start=10&amp;sa=N"" ></a>
                              <a href=""/url?q=http://test.com?q=dddddd&amp;ie=UTF-8&amp;ei=VbVSXbSfNYGT8AOhgZfIBQ&amp;start=10&amp;sa=N""aria-label=""Next page"" ></a>";
        }

        [Fact]
        public async Task it_should_return_empty_string_when_result_not_found()
        {
            var loaderResult = new LoadResult();
            Mock.Get(loader).Setup(l => l.LoadAsync(It.IsAny<string>())).Returns(Task.FromResult(loaderResult));

            var result = await strategy.SearchAsync("test.com", "anything", 10);

            Assert.True(result.ContentStr == string.Empty);
        }


        [Fact]
        public async Task it_should_return__correct_result_when_num_results_less_then_links_count()
        {
            var loaderResult = new LoadResult
            {
                ContentStr =contentStr,
                ErrorCode = System.Net.HttpStatusCode.OK
            };

            Mock.Get(loader).Setup(l => l.LoadAsync(It.IsAny<string>())).Returns(Task.FromResult(loaderResult));

            var result = await strategy.SearchAsync("test.com", "anything", 1);

            Assert.True(result.ContentStr == "1");
        }

        [Fact]
        public async Task it_should_return__correct_result_when_num_results_more_then_links_count()
        {
            var loaderResult = new LoadResult
            {
                ContentStr = contentStr,
                ErrorCode = System.Net.HttpStatusCode.OK
            };

            Mock.Get(loader).Setup(l => l.LoadAsync(It.IsAny<string>())).Returns(Task.FromResult(loaderResult));

            var result = await strategy.SearchAsync("test.com", "anything", 5);

            Assert.True(result.ContentStr == "1,2");
        }

        [Fact]
        public async Task it_should_throw_ArgumentNullException_when_company_url_is_null_or_empty()
        {           
            Assert.ThrowsAsync<ArgumentNullException>(async () => await strategy.SearchAsync(null,"anything",10));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await strategy.SearchAsync("", "anything", 10));
        }

        [Fact]
        public async Task it_should_throw_ArgumentNullException_when_text_is_null()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await strategy.SearchAsync("company.com.au", null, 10));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await strategy.SearchAsync("company.com.au", "", 10));
        }

        [Fact]
        public async Task it_should_throw_ArgumentNullException_when_num_results_is_zero_or_less()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await strategy.SearchAsync("company.com.au", "anything", 0));
            Assert.ThrowsAsync<ArgumentNullException>(async () => await strategy.SearchAsync("company.com.au", "anything", -5));
        }
    }
}
