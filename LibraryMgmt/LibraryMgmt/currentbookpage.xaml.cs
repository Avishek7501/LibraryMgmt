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
	public partial class currentbookpage : ContentPage
	{
		public currentbookpage ()
		{
			InitializeComponent ();
		}

        private async void OnBookTapped(object sender, EventArgs e)
        {
			var tappedEventarg = (TappedEventArgs)e;
			var booktitle = tappedEventarg.Parameter as string;
            await Navigation.PushModalAsync(new currentbookdetail(booktitle));
        }
    }
}