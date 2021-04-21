using Microcharts;
using MobilApp.ViewModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphicMeasurementPage : ContentPage
    {
        GraphicMeasurementViewModel _viewModel;

        public GraphicMeasurementPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new GraphicMeasurementViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}