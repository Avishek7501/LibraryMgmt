

using System;

namespace LibraryMgmt.Models
{
    public class Book
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ReturnedDate {  get; set; }
    }
}
