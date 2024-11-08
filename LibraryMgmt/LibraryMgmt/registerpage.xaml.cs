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
	public partial class registerpage : ContentPage
	{
		public registerpage ()
		{
			InitializeComponent ();
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
           /* if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(contact) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please fill out all fields.", "OK");
                return;
            }
           */

            // Optionally: Add code to save the user data to the database here
            // Example:
            // SaveUserToDatabase(name, address, contact, email, username, password);

            // Display a success message
            await DisplayAlert("Success", "Registration completed!", "OK");

            // Navigate to the login page or another page if necessary
            await Navigation.PushModalAsync(new loginpage());
        }

        // Example of a function to save user data to the database (replace with actual implementation)
        private void SaveUserToDatabase(string name, string address, string contact, string email, string username, string password)
        {
            // Database code to save user data
            // This method should be replaced with actual database logic
        }
    }
}