using System;
using Xamarin.Forms;

namespace LibraryMgmt
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void getstarted(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new loginpage());
        }
    }
}
