using SearchEngine.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class PageLoader : IPageLoader
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<LoadResult> LoadAsync(string url)
        {
            var result = new LoadResult();

            var response = await client.GetAsync(url);

            result.ErrorCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                var byteArr = await response.Content.ReadAsByteArrayAsync();

                result.ContentStr = Encoding.UTF8.GetString(byteArr, 0, byteArr.Length - 1);
            }

            return result;
        }
    }
}
