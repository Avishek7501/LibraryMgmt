using System.ComponentModel.DataAnnotations;

namespace LibraryMgmtAPI.Model
{
    public class Book
    {
        public int BookID { get; set; }  // Primary Key
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }  
    }
}
