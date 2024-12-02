using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMgmt.Models
{
    public class ReturnHistoryDetail
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
