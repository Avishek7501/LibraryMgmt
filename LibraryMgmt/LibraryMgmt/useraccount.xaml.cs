using System;
using LibraryMgmt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class useraccount : ContentPage
    {
        private readonly Member _currentMember;

        public useraccount(Member member)
        {
            InitializeComponent();

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");
            }

            _currentMember = member;

            // Bind member details to UI
            BindMemberDetails();
        }

        private void BindMemberDetails()
        {
            // Dynamically update UI with member details
            UserNameLabel.Text = _currentMember.Name;
            EmailLabel.Text = _currentMember.Email;
            ContactLabel.Text = _currentMember.Contact;
            AddressLabel.Text = _currentMember.Address;
        }

        private async void Contactlib(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new contact_insidepage());
        }

        private async void logout(object sender, EventArgs e)
        {
            // Clear the session (if using a session manager)
           

            // Navigate back to login page
            await Navigation.PushModalAsync(new loginpage());
        }
    }
}
