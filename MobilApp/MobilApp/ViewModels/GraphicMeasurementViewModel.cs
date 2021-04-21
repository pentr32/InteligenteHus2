using MobilApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microcharts;
using Entry = Microcharts.ChartEntry;
using SkiaSharp;

namespace MobilApp.ViewModels
{
    public class GraphicMeasurementViewModel : BaseViewModel
    {
        public ObservableCollection<THMeasurement> THMeasurements;
        public List<Entry> Entries;
        private Chart measurementChart;
        public Chart MeasurementChart
        {
            get => measurementChart;
            set => SetProperty(ref measurementChart, value);
        }

        public Command LoadMeasurementsCommand { get; }

        public GraphicMeasurementViewModel()
        {
            Title = "Graphic Measurements";
            THMeasurements = new ObservableCollection<THMeasurement>();
            Entries = new List<Entry>();
            LoadMeasurementsCommand = new Command(async () => await ExecuteLoadAllMeasurementsCommand());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
        }

        async Task ExecuteLoadAllMeasurementsCommand()
        {
            IsBusy = true;

            try
            {
                THMeasurements.Clear();
                Entries.Clear();

                var items = await _thService.GetAllMeasurementsAsync();
                foreach (var item in items)
                {
                    THMeasurements.Add(item);
                    Entries.Add(new Entry(200)
                    {
                        Color = SKColor.Parse("#00BFFF"),
                        Label = nameof(item.Temperature),
                        ValueLabel = item.Temperature.ToString()
                    });
                }

                MeasurementChart = new RadialGaugeChart { Entries = Entries };
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