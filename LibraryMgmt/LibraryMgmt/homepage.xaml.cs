using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;
using LibraryMgmt.Services;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class homepage : ContentPage
    {
        private readonly ApiService _apiService;

        private  Member _currentMember;
       
        

        public homepage(Member member)
        {
            InitializeComponent();

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Current member cannot be null.");
            }
            _currentMember = member;

            BindingContext = this;
            _apiService = new ApiService();

            // Set up bottom navigation with current member
            WelcomeMessage.Text = $"Welcome, {_currentMember.Name} ";
            // Fetch notification count
            FetchNotificationCount();
        }

        private async void OnNotificationTapped(object sender, EventArgs e)
        {
            // Navigate to notification page and fetch detailed notifications there
            await Navigation.PushModalAsync(new notificationpage(_currentMember));
        }

        private async void OnCurrentBooksTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new currentbookpage(_currentMember));
        }

        private async void OnReturnHistoryTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new returnhistorypage(_currentMember));
        }

        private async void FetchNotificationCount()
        {
            try
            {
                // Log the MemberID being used
                Console.WriteLine($"Fetching notification count for MemberID: {_currentMember.MemberID}");
                var count = 0;
                // Fetch notification count from the API
                count =  await _apiService.GetAsync<int>($"Notification/Count/{_currentMember.MemberID}");
                Console.WriteLine(count.ToString());
                // Update UI
                NotificationLabel.Text = $"You have {count} notification(s) for return due dates";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error fetching notification count: {ex.Message}");

                // Display error to user and fallback to 0 notifications
                await DisplayAlert("Error", "Failed to fetch notification count", "OK");
                NotificationLabel.Text = "You have 0 notification(s) for return due dates";
            }
        }
    }
}
