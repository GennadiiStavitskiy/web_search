using SearchEngine.Models;
using System.Threading.Tasks;

namespace SearchEngine
{
    public interface ISearchStrategy
    {
        Task<LoadResult> SearchAsync(string url, string text, int numResults);
    }
}
