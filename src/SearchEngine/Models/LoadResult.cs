using System.Net;

namespace SearchEngine.Models
{
    public class LoadResult
    {
        public string ContentStr { get; set; }
        public HttpStatusCode ErrorCode { get; set; }

        public bool IsSuccess
        {
            get
            {
                return ErrorCode == HttpStatusCode.OK;
            }
        }

    }
}
