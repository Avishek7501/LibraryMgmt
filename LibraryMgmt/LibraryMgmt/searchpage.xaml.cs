using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class searchpage : ContentPage
    {
        // Hardcoded list of book titles for demo purposes
        private readonly string[] books = { "Advanced Xamarin", "Book 1", "Book 2", "Book 3" };
        private string foundBookTitle = string.Empty;



        public searchpage()
        {
            InitializeComponent();
        }

        private void OnSearchTapped(object sender, EventArgs e)
        {
            string query = SearchEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(query))
            {
                // Assume no book is found initially
                bool bookFound = false;
                BookResultFrame.IsVisible = false; // Hide book result frame initially

                // Search for the book
                foreach (var book in books)
                {
                    if (book.Equals(query, StringComparison.OrdinalIgnoreCase))
                    {
                        bookFound = true;
                        foundBookTitle = book;
                        BookNameLabel.Text = book; // Set the found book name
                        
                        BookResultFrame.IsVisible = true; // Show the frame with book details
                        ResultStatusLabel.Text = "Found:"; // Set status label text
                        break;
                    }
                }

                // Update the ResultStatusLabel based on whether a book was found
                if (!bookFound)
                {
                    ResultStatusLabel.Text = "No books found.";

                }

                // Show the SearchResults section
                SearchResults.IsVisible = true;
            }
            else
            {
                // Hide results if search is empty
                SearchResults.IsVisible = false;
            }
        }


        private async void OnResultTapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(foundBookTitle))
            {
                await Navigation.PushModalAsync(new bookdetailpage(foundBookTitle));
            }
           
        }
    }
}
	
