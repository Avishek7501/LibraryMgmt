using System;
using Xamarin.Forms;
using LibraryMgmt.Services;
using LibraryMgmt.Models;// Reference to the local Member model

namespace LibraryMgmt
{
    public partial class loginpage : ContentPage
    {
        private readonly ApiService _apiService;

        public loginpage()
        {
            InitializeComponent();
            _apiService = new ApiService(); // Initialize the API service
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            try
            {
                // Prepare login request payload
                var loginPayload = new
                {
                    Username = username,
                    Password = password
                };

                // Send API request to validate login
                var member = await _apiService.PostAsync<Member>("Auth/Login", loginPayload);

                if (member != null)
                {
                    // Set the global member for navigation
                    
                    Console.WriteLine($"Member: {member.Name}, {member.MemberID}");
                    // Login successful
                    await DisplayAlert("Success", "Login successful!", "OK");
                    bottomnavigation.SetGlobalMember(member);
                    // Navigate to homepage 
                    await Navigation.PushModalAsync(new homepage(member),false);
                    UsernameEntry.Text = "";
                    PasswordEntry.Text = "";

                }
                else
                {

                    // Invalid credentials
                    await DisplayAlert("Error", "Invalid username or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error", $"Login failed: {ex.Message}", "OK");
            }
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new contact_outsidepage());
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            // Navigate to registration page
            await Navigation.PushModalAsync(new registerpage());
        }
    }
}
