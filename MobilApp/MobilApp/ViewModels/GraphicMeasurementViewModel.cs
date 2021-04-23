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
using System.Linq;

namespace MobilApp.ViewModels
{
    public class GraphicMeasurementViewModel : BaseViewModel
    {
        #region Properties
        public Command LoadMeasurementsCommand { get; set; }

        private MeasurementFilterBy selectedFilter;
        public MeasurementFilterBy SelectedFilter
        {
            get => selectedFilter;
            set
            {

                if (SetProperty(ref selectedFilter, value))
                {
                    ExecuteLoadAllMeasurementsCommand();
                }
            }
        }
        public List<string> PickerFilters
        {
            get
            {
                return Enum.GetNames(typeof(MeasurementFilterBy)).ToList();
            }
        }

        private Chart temperatureChart;
        private Chart humidityChart;
        public Chart TemperatureChart
        {
            get => temperatureChart;
            set => SetProperty(ref temperatureChart, value);
        }

        public Chart HumidityChart
        {
            get => humidityChart;
            set => SetProperty(ref humidityChart, value);
        }

        #endregion Properties

        #region Constructor
        public GraphicMeasurementViewModel()
        {
            Title = "Graphic Measurements";

            SelectedFilter = MeasurementFilterBy.ByNewestDay;
            LoadMeasurementsCommand = new Command(() => ExecuteLoadAllMeasurementsCommand());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        #endregion Constructor

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
        }

        async void ExecuteLoadAllMeasurementsCommand()
        {
            IsBusy = true;

            try
            {

                var measurements = await _thService.GetAllMeasurementsAsync(SelectedFilter);
                PopulateCharts(measurements);
                
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

        void PopulateCharts(IEnumerable<THMeasurement> measurements)
        {
            List<Entry> TemperaturEntries = new List<Entry>();
            List<Entry> HumidityEntries = new List<Entry>();

            foreach (var item in measurements)
            {
                #region Add entries for temperature chart
                TemperaturEntries.Add(new Entry(item.Temperature)
                {
                    Color = SKColor.Parse("#FFFFFF"),
                    TextColor = SKColor.Parse("#FFFFFF"),
                    Label = item.UpdatedMeasurementTime.ToString(),
                    ValueLabelColor = SKColor.Parse("#FFFFFF"),
                    ValueLabel = item.Temperature.ToString()
                });
                #endregion Add entries for temperature chart

                #region Add entries for humidity chart
                HumidityEntries.Add(new Entry(item.Humidity)
                {
                    Color = SKColor.Parse("#FFFFFF"),
                    TextColor = SKColor.Parse("#FFFFFF"),
                    Label = item.UpdatedMeasurementTime.ToString(),
                    ValueLabelColor = SKColor.Parse("#FFFFFF"),
                    ValueLabel = item.Humidity.ToString()
                });
                #endregion Add entries for temperature chart
            }

            #region Populate Charts
            TemperatureChart = new LineChart
            {
                Entries = TemperaturEntries,
                LabelTextSize = 36,
                LineSize = 8,
                PointMode = PointMode.Circle,
                LineMode = LineMode.Straight,
                PointSize = 18,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Vertical,
                AnimationProgress = 0.3f,
                BackgroundColor = SKColor.Parse("#0b0b44"),
                LabelColor = SKColor.Parse("#FFFFFF")
            };

            HumidityChart = new BarChart
            {
                Entries = HumidityEntries,
                LabelTextSize = 36,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Vertical,
                AnimationProgress = 0.3f,
                BackgroundColor = SKColor.Parse("#0b0b44"),
                LabelColor = SKColor.Parse("#FFFFFF")
            };
            #endregion Populate Charts
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