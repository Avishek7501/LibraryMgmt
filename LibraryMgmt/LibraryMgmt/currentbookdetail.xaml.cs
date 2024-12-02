using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class currentbookdetail : ContentPage
    {
        public currentbookdetail(IssuedBookDetail book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            InitializeComponent();
            Console.WriteLine($"Book Details: Title={book.Title}, IssuedDate={book.IssueDate}, ReturnDate={book.ReturnDate}");

            // Set the book object as the BindingContext for the page
            BindingContext = book;
            
        }

        

        
    }

}
