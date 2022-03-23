using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculatorMobile.Services
{
    public class AlertService : IAlertService
    {
        public void Alert(string title, string message)
        {
            App.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
