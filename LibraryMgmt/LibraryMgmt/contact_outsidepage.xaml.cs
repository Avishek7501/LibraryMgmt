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
    public partial class contact_outsidepage : ContentPage
    {
        public contact_outsidepage()
        {
            InitializeComponent();
        }

        private async void OnGoToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new loginpage());
        }
    }
}