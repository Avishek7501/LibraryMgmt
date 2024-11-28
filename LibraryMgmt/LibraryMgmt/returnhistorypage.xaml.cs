using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;
using LibraryMgmt.Services;
using System.Collections.Generic;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class returnhistorypage : ContentPage
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Book> ReturnedBooks { get; set; }
        private Member _currentMember;

        public returnhistorypage(Member currentMember)
        {
            InitializeComponent();
            _apiService = new ApiService();
            _currentMember = currentMember;

            // Initialize the collection and bind the page's BindingContext
            ReturnedBooks = new ObservableCollection<Book>();
            BindingContext = this;

            // Fetch the return history
            FetchReturnHistory();
        }

        private async void FetchReturnHistory()
        {
            try
            {
                // Fetch returned books from the API for the current member
                var returnedBooks = await _apiService.GetAsync<ObservableCollection<Book>>($"ReturnHistory/Member/{_currentMember.MemberID}");
                foreach (var book in returnedBooks)
                {
                    ReturnedBooks.Add(book);
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine($"Error fetching current books: {ex.Message}");
                await DisplayAlert("Error", "Failed to fetch return history. Please try again later.", "OK");
            }
        }

        private async void OnBookTapped(object sender, EventArgs e)
        {
            var tappedEventArgs = (TappedEventArgs)e;
            var book = tappedEventArgs.Parameter as Book;

            if (book != null)
            {
                await Navigation.PushModalAsync(new returnhistorydetail(book));
            }
        }
    }
}
