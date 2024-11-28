using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;
using LibraryMgmt.Services;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class currentbookpage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly Member _currentMember;

        public ObservableCollection<Book> CurrentBooks { get; set; }

        public currentbookpage(Member member)
        {
            InitializeComponent();

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }

            _apiService = new ApiService();
            _currentMember = member;

            CurrentBooks = new ObservableCollection<Book>();
            BindingContext = this;

            FetchCurrentBooks();
        }

        private async void FetchCurrentBooks()
        {
            try
            {
                // Fetch the current books for the member
                var books = await _apiService.GetAsync<ObservableCollection<Book>>($"IssuedBooks/Member/{_currentMember.MemberID}");
                if (books != null)
                {
                    foreach (var book in books)
                    {
                        CurrentBooks.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching current books: {ex.Message}");
                await DisplayAlert("Error", "Failed to fetch current books. Please try again later.", "OK");
            }
        }

        private async void OnBookTapped(object sender, EventArgs e)
        {
            var tappedEventArg = (TappedEventArgs)e;
            var book = tappedEventArg.Parameter as Book;
            if (book != null)
            {
                await Navigation.PushModalAsync(new currentbookdetail(book));
            }
        }
    }
}
