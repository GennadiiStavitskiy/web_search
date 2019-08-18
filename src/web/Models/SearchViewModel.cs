using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class SearchViewModel
    {
        [Required]
        public string Text { get; set; } = "e-settlements";
        [Required]
        public string Url { get; set; } = "www.sympli.com.au";
        public string Engine { get; set; } = "google";
        public string Result { get; set; }       
    }
}