using MobilApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobilApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}