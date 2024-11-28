using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryMgmt.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryMgmt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class bookdetailpage : ContentPage
    {
        public bookdetailpage(Book books )
        {
            InitializeComponent();
            if (books != null)
            {
                BookTitleLabel.Text = books.Title;
                BookAuthorLabel.Text = books.Author;
                BookGenreLabel.Text = books.Genre;
                BookPublishedDateLabel.Text = books.PublishedDate.ToString("yyyy-MM-dd");
            }
        }


    }
}