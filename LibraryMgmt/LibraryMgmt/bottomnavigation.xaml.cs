using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryMgmt.Models;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class bottomnavigation : ContentView
    {
        public static Member CurrentMember { get; private set; }
        public bottomnavigation()
        {
            InitializeComponent();
           
        }

        public static void SetGlobalMember(Member member)
        {
            CurrentMember = member ?? throw new ArgumentNullException(nameof(member), "Member cannot be null.");
        }
        

        private async void OnHomeTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new homepage(CurrentMember));
        }

        private async void OnSearchTapped(object sender, EventArgs e)
        {
          await Navigation.PushModalAsync (new searchpage());
        }

        private async void OnProfileTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new useraccount(CurrentMember));
        }


    }
}