using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class currentbookdetail : ContentPage
    {
        public currentbookdetail(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            InitializeComponent();

            // Set the book object as the BindingContext for the page
            BindingContext = book;
        }
    }
}
