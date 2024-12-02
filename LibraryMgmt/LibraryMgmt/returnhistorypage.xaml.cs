using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;
using LibraryMgmt.Services;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class returnhistorypage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly Member _currentMember;

        public ObservableCollection<ReturnHistoryDetail> ReturnedBooks { get; set; }

        public returnhistorypage(Member member)
        {
            InitializeComponent();

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }

            _apiService = new ApiService();
            _currentMember = member;

            ReturnedBooks = new ObservableCollection<ReturnHistoryDetail>();
            BindingContext = this;

            FetchReturnHistory();
        }

        private async void FetchReturnHistory()
        {
            try
            {
                var returnedBooks = await _apiService.GetAsync<ObservableCollection<ReturnHistoryDetail>>($"ReturnHistory/Member/{_currentMember.MemberID}");

                if (returnedBooks != null)
                {
                    foreach (var book in returnedBooks)
                    {
                        ReturnedBooks.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching return history: {ex.Message}");
                await DisplayAlert("Error", "Failed to fetch return history. Please try again later.", "OK");
            }
        }

        private async void OnBookTapped(object sender, EventArgs e)
        {
            var tappedEventArgs = (TappedEventArgs)e;
            var book = tappedEventArgs.Parameter as ReturnHistoryDetail;

            if (book != null)
            {
                await Navigation.PushModalAsync(new returnhistorydetail(book));
            }
        }
    }
}
