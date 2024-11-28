using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Services; // Assuming ApiService is here
using LibraryMgmt.Models;   // Assuming the Notification model is here

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class notificationpage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly Member _currentMember;

        public List<Notification> Notifications { get; set; }

        public notificationpage(Member member)
        {
            InitializeComponent();

            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");

            _currentMember = member;
            _apiService = new ApiService();

            BindingContext = this;

            FetchNotifications();
        }

        private async void FetchNotifications()
        {
            try
            {
                Console.WriteLine($"Fetching notifications for MemberID: {_currentMember.MemberID}");

                // Fetch notifications for the current member
                var notifications = await _apiService.GetAsync<List<Notification>>($"Notification/Member/{_currentMember.MemberID}");

                if (notifications != null && notifications.Count > 0)
                {
                    Notifications = notifications;
                    OnPropertyChanged(nameof(Notifications));
                }
                else
                {
                    await DisplayAlert("Info", "No notifications available.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching notifications: {ex.Message}");
                await DisplayAlert("Error", "Failed to fetch notifications. Please try again later.", "OK");
            }
        }
    }
}
