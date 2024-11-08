using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static LibraryMgmt.homepage;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class notificationpage : ContentPage
    {


        public List<Notification> Notifications { get; set; }

        public notificationpage(List<homepage.Notification> notifications)
        {
            InitializeComponent();

            Notifications = notifications;
            BindingContext = this;
            

        }


    }

    
    }
