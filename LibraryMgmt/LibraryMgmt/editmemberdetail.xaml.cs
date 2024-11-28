using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;
using LibraryMgmt.Services;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class editmemberdetail : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly Member _currentMember;

        public editmemberdetail(Member member)
        {
            InitializeComponent();
            _apiService = new ApiService();

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }

            _currentMember = member;
            BindingContext = _currentMember;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(ContactEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                await DisplayAlert("Error", "All fields except Username must be filled.", "OK");
                return;
            }

            // Prepare updated member object
            _currentMember.Name = NameEntry.Text;
            _currentMember.Address = AddressEntry.Text;
            _currentMember.Contact = ContactEntry.Text;
            _currentMember.Email = EmailEntry.Text;

            try
            {
                // Call the API to update member details
                await _apiService.PutAsync($"Members/{_currentMember.MemberID}", _currentMember);
                await DisplayAlert("Success", "Your details have been updated.", "OK");
                MessagingCenter.Send(this, "MemberUpdated", _currentMember);
                // Navigate back or refresh the profile page
                await Navigation.PopModalAsync();


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update details: {ex.Message}", "OK");
            }
        }
    }
}
