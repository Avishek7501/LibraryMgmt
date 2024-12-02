using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMgmt.Models
{
    public class IssuedBookDetail
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
