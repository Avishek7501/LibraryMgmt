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
	public partial class returnhistorydetail : ContentPage
	{
		public returnhistorydetail (string returnedbooks)
		{
			InitializeComponent ();
			BookLabelTitle.Text = returnedbooks;

			
		}

		
	}
}