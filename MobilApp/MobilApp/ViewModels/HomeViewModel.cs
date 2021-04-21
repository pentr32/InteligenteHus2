using MobilApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobilApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private THMeasurement thMeasurement;
        public THMeasurement THMeasurement
        {
            get => thMeasurement;
            set => SetProperty(ref thMeasurement, value);
        }

        public Command LoadMeasurementsCommand { get; }

        public HomeViewModel()
        {
            Title = "Measurements";
            LoadMeasurementsCommand = new Command(async () => await ExecuteLoadMeasurementsCommand());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
        }

        async Task ExecuteLoadMeasurementsCommand()
        {
            IsBusy = true;

            try
            {
                //THMeasurements.Clear();
                THMeasurement = await _thService.GetCurrentMeasurementAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public void Dispose()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
    }
}
