using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryMgmtAPI.Model
{
    public class BookCopy
    {
        
        public int CopyID { get; set; }  // Primary Key
        public int BookID { get; set; }  // Foreign Key to Books table
        [Required]
        public bool IsIssued { get; set; } = false;   // Whether the copy is issued
    }
}
