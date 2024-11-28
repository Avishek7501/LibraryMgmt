using LibraryMgmt.Models;
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
		public returnhistorydetail (Book book)
		{
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }
            InitializeComponent ();

			BindingContext = book;

			
		}

		
	}
}