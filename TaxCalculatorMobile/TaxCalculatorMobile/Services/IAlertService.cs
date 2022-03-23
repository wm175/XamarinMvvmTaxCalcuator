using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculatorMobile.Services
{
    public interface IAlertService
    {
        void Alert(string title, string message);
    }
}
