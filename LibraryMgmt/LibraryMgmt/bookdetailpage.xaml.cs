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
    public partial class bookdetailpage : ContentPage
    {
        public bookdetailpage( string Searchedbook)
        {
            InitializeComponent();
            BookLabelTitle.Text = Searchedbook;
        }


    }
}