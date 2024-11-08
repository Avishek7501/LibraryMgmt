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
    public partial class bottomnavigation : ContentView
    {
        public bottomnavigation()
        {
            InitializeComponent();
        }

        private async void OnHomeTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new homepage());
        }

        private async void OnSearchTapped(object sender, EventArgs e)
        {
          await Navigation.PushModalAsync (new searchpage());
        }

        private async void OnProfileTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new useraccount());
        }


    }
}