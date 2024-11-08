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
	public partial class useraccount : ContentPage
	{
		public useraccount ()
		{
			InitializeComponent ();
		}

        private async void Contactlib(object sender, EventArgs e)
        {
			await Navigation.PushModalAsync(new contact_insidepage());
        }

		private async void logout(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new loginpage());
		}
    }
}