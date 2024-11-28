using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LibraryMgmt.Models; // Ensure you have the Book model in the correct namespace
using LibraryMgmt.Services; // For ApiService
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class searchpage : ContentPage
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Book> books;
        private Book selectedBook;

        public searchpage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnSearchTapped(object sender, EventArgs e)
        {
            string query = SearchEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    string encodedQuery = Uri.EscapeDataString(query);

                    // Fetch books from the API
                    books = await _apiService.GetAsync<ObservableCollection<Book>>($"Books/Search?name={encodedQuery}");

                    if (books != null && books.Any())
                    {
                        // Display the first matching book for simplicity
                        selectedBook = books.FirstOrDefault();

                        // Update UI elements with fetched book details
                        BookNameLabel.Text = selectedBook?.Title ?? "Unknown Title";
                        BookResultFrame.IsVisible = true; // Show the result frame
                        ResultStatusLabel.Text = $"Found {books.Count} book(s)";
                    }
                    else
                    {
                        ResultStatusLabel.Text = "No books found.";
                        BookResultFrame.IsVisible = false; // Hide the result frame
                    }

                    // Show the SearchResults section
                    SearchResults.IsVisible = true;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to search for books: {ex.Message}", "OK");
                    ResultStatusLabel.Text = "Error fetching results.";
                    BookResultFrame.IsVisible = false; // Hide the result frame
                }
            }
            else
            {
                // Hide results if search is empty
                SearchResults.IsVisible = false;
            }
        }

        private async void OnResultTapped(object sender, EventArgs e)
        {
            if (selectedBook != null)
            {
                // Navigate to book detail page with the selected book details
                await Navigation.PushModalAsync(new bookdetailpage(selectedBook));
            }
        }
    }
}
