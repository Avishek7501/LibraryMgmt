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
	public partial class returnhistorypage : ContentPage
	{
		public returnhistorypage ()
		{
			InitializeComponent ();
		}

        private async void OnBookTapped(object sender, EventArgs e)
		{
			var onTappedeventargs = (TappedEventArgs)e;
			var returnedbook = onTappedeventargs.Parameter as string;
			await Navigation.PushModalAsync(new returnhistorydetail(returnedbook));
		}
    }
}