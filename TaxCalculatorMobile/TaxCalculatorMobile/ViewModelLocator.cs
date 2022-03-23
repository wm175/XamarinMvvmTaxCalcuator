using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Common.Models;
using TaxCalculator.Common;
using TaxCalculatorMobile.ViewModels;
using TaxCalculatorMobile.Services;
using TaxCalculator.Calculators;
using Xamarin.Forms;

namespace TaxCalculatorMobile
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }
        public TaxCalculatorViewModel TaxCalculatorVm
        {
            get
            {
                Order someOrder = new Order();
                //Assumption: all orders will be placed to and from the US
                someOrder.FromLocation.Country = someOrder.ToLocation.Country = "US";

                return new TaxCalculatorViewModel(someOrder, TinyIoC.TinyIoCContainer.Current.Resolve<ITaxService>());
            }
        }
    }
}
