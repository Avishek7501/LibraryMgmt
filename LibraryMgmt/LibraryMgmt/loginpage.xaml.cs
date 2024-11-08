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
	public partial class loginpage : ContentPage
	{
		public loginpage ()
		{
			InitializeComponent ();
		}

        private async void OnLoginClicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new homepage());
           
        }

        private async void OnForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync (new contact_outsidepage());
        }
    }
}