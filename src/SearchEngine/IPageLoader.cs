using System.Threading.Tasks;
using SearchEngine.Models;

namespace SearchEngine
{
    public interface IPageLoader
    {
        Task<LoadResult> LoadAsync(string url);
    }
}