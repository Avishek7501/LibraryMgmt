using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Services;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registerpage : ContentPage
    {
        private readonly ApiService _apiService;

        public registerpage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Retrieve user inputs
            string name = NameEntry.Text;
            string address = AddressEntry.Text;
            string contact = ContactEntry.Text;
            string email = EmailEntry.Text;
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(contact) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please fill out all fields.", "OK");
                return;
            }

            // Create a member object
            var newMember = new
            {
                Name = name,
                Address = address,
                Contact = contact,
                Email = email,
                Username = username,
                Password = password
            };

            try
            {
                // Call the API
                var result = await _apiService.PostAsync<object>("Members", newMember);

                // Show success message
                await DisplayAlert("Success", "Registration completed!", "OK");

                // Optionally navigate to a login page or reset form fields
                await Navigation.PushModalAsync(new loginpage());
            }
            catch (Exception ex)
            {
                // Show error message
                await DisplayAlert("Error", $"Failed to register: {ex.Message}", "OK");
            }
        }
    }
}
