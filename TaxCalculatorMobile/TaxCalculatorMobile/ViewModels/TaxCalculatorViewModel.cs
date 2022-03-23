using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TaxCalculator.Common;
using TaxCalculator.Common.Models;
using TaxCalculatorMobile.Services;
using Xamarin.Forms;

namespace TaxCalculatorMobile.ViewModels
{
    public class TaxCalculatorViewModel : INotifyPropertyChanged
    {
        private List<string> _statesList;
        private ITaxService _taxService;
        private Order _order;
        private ICommand _calculateCommand;
        private decimal _taxOwed;
        private bool _isTaxCalculated;

        public TaxCalculatorViewModel(Order order, ITaxService taxService)
        {
            this._order = order;
            this._taxService = taxService;
            this._isTaxCalculated = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CalculateCommand
        {
            get
            {
                if (_calculateCommand == null)
                    _calculateCommand = new Command(() =>
                    {
                        var errors = GetInputErrors();
                        if (errors.Count > 0)
                            TinyIoC.TinyIoCContainer.Current.Resolve<IAlertService>().Alert("Please fix errors", String.Join("\n", errors));
                        else
                            this.CalculateTaxes();
                    });

                return _calculateCommand;
            }
        }

        public decimal OrderTotal { 
            get {
                return _order.OrderTotal;
            }
            set
            {
                _order.OrderTotal = value;
                OnPropertyChanged(nameof(OrderTotal));
                this.IsTaxCalculated = false;
            }
        }
        public decimal ShippingCost {
            get
            {
                return _order.ShippingCost;
            }
            set
            {
                _order.ShippingCost = value;
                OnPropertyChanged(nameof(ShippingCost));
                this.IsTaxCalculated = false;
            }
        }

        public string FromState {
            get 
            {
                return _order.FromLocation.State;
            }
            set
            {
                if (value != null)
                {
                    _order.FromLocation.State = value;
                    OnPropertyChanged(nameof(FromState));
                    this.IsTaxCalculated = false;
                }
            }
        }
        public string FromZip
        {
            get
            {
                return _order.FromLocation.Zipcode;
            }
            set
            {
                if (value.Contains("."))
                    value = value.Replace(".", "");
                _order.FromLocation.Zipcode = value;
                OnPropertyChanged(nameof(FromZip));
                this.IsTaxCalculated = false;
            }
        }

        public string ToState
        {
            get
            {
                return _order.ToLocation.State;
            }
            set
            {
                if (value != null)
                {
                    _order.ToLocation.State = value;
                    OnPropertyChanged(nameof(ToState));
                    this.IsTaxCalculated = false;
                }
            }
        }
        public string ToZip
        {
            get
            {
                return _order.ToLocation.Zipcode;
            }
            set
            {
                if (value.Contains("."))
                    value = value.Replace(".", "");
                _order.ToLocation.Zipcode = value;
                OnPropertyChanged(nameof(ToZip));
                this.IsTaxCalculated = false;
            }
        }

        public List<string> StatesList
        {
            get
            {
                if (_statesList == null)
                    _statesList = BuildStatesList();
                return _statesList;
            }
        }

        public bool IsTaxCalculated
        {
            get
            {
                return _isTaxCalculated;
            }
            private set
            {
                _isTaxCalculated = value;
                OnPropertyChanged(nameof(IsTaxCalculated));
            }
        }

        public string TaxOwedString
        {
            get
            {
                return "$" + _taxOwed;
            }
        }

        protected void CalculateTaxes()
        {
            var tax = _taxService.CalculateTaxForOrder(_order);

            if(tax >= 0)
            {
                this.IsTaxCalculated = true;
                this._taxOwed = tax;
                OnPropertyChanged(nameof(TaxOwedString));
            }
            else 
            {
                TinyIoC.TinyIoCContainer.Current.Resolve<IAlertService>().Alert("Oops!", "Something went wrong. Please check your inputs and try again.");
            }
        }

        protected List<string> GetInputErrors()
        {
            List<string> errors = new List<string>();

            if (_order.OrderTotal < 0)
                errors.Add("- Order Total cannot be negative");
            if (_order.ShippingCost < 0)
                errors.Add("- Shipping cost cannot be negative");
            if (_order.FromLocation.State == "")
                errors.Add("- From state unselected");
            if (_order.FromLocation.Zipcode.Length != 5)
                errors.Add("- From zipcode must be 5 digits");
            if (_order.ToLocation.State == "")
                errors.Add("- To state unselected");
            if (_order.ToLocation.Zipcode.Length != 5)
                errors.Add("- To zipcode must be 5 digits");

            return errors;
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }

        private List<string> BuildStatesList()
        {
           return new List<string>()
            {
                "AL",
                "AK",
                "AZ",
                "AR",
                "CA",
                "CO",
                "CT",
                "DE",
                "DC",
                "FL",
                "GA",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "OH",
                "OK",
                "OR",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "PA",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VT",
                "VA",
                "WA",
                "WV",
                "WI",
                "WY",
            };
        }
    }
}
