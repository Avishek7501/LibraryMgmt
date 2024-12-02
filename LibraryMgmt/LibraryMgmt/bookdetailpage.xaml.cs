using System;
using LibraryMgmt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class bookdetailpage : ContentPage
    {
        public bookdetailpage(Book book)
        {
            InitializeComponent();

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            BindingContext = book; // Set the book object as the BindingContext for data binding
        }
    }
}
