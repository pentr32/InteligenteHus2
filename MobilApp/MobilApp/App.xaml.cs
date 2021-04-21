using MobilApp.Repository;
using MobilApp.Services;
using MobilApp.Views;
using System;
using TinyIoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";

            var container = TinyIoCContainer.Current;
            container.Register<IGenericRepository, GenericRepository>();
            container.Register<ITHService, THService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
