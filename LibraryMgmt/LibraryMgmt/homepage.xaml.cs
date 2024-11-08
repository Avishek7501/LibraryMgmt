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
    public partial class homepage : ContentPage
    {

        private int _notificationCount;
        public string NotificationMessage => $"You have {_notificationCount} notification(s) for return due date";

        public homepage()
        {
            InitializeComponent();
            BindingContext = this;

            // Fetch the total notification count when the page is created
            FetchNotificationCount();




        }

        private async void OnNotificationTapped(object sender, EventArgs e)
        {
            var notifications = await GetNotificationsFromDatabase();
            // Handle notification tap
            await Navigation.PushModalAsync(new notificationpage(notifications));
            // Navigate to the notification details page
            // Example: await Navigation.PushAsync(new NotificationDetailsPage());
        }

        private async void OnCurrentBooksTapped(object sender, EventArgs e)
        {


            // Handle current books tap
            await Navigation.PushModalAsync(new currentbookpage());
            // Navigate to the Current Books page

        }

       

        private async void OnReturnHistoryTapped(object sender, EventArgs e)
        {
            // Handle return history tap
           await Navigation.PushModalAsync(new returnhistorypage());
            
        }

        private async void FetchNotificationCount()
        {
            
            var notifications = await GetNotificationsFromDatabase();
            _notificationCount = notifications.Count;

            // Notify the UI to update the notification message
            OnPropertyChanged(nameof(NotificationMessage));
        }

        private Task<List<Notification>> GetNotificationsFromDatabase()
        {
            
            return Task.FromResult(new List<Notification>
            {
                new Notification { Message = "Return due in 2 days for 'Book 1'" },
                new Notification { Message = "Return due in 3 days for 'Book 2'" },
                new Notification { Message = "Return due in 5 days for 'Book 4'" },
                new Notification { Message = "Return due in 7 days for 'Book 6'" }
            });
        }



        public class Notification
        {
            public string Message { get; set; }
        }



    }
}
    
