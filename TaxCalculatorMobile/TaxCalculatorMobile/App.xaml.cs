using System;
using TaxCalculatorMobile.Services;
using TaxCalculatorMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TinyIoC;
using TaxCalculator.Calculators;
using TaxCalculator.Common;
using System.Net.Http;

namespace TaxCalculatorMobile
{
    public partial class App : Application
    {
        internal static Lazy<HttpClient> Client = new Lazy<HttpClient>(() => { return new HttpClient(); });
        public App()
        {
            InitializeComponent();

            this.RegisterDependencies();

            MainPage = new TaxCalculatorPage();
        }

        private void RegisterDependencies()
        {
            TinyIoCContainer.Current.Register<ITaxCalculator>(new TaxJarCalculator(Client.Value));
            TinyIoCContainer.Current.Register<ITaxService, TaxService>();
            TinyIoCContainer.Current.Register<IAlertService, AlertService>();
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
