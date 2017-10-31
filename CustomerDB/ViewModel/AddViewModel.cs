using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using CustomerDB.Model;
using CustomerDB.View;
using System.Windows;
using System.ComponentModel;
using System;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace CustomerDB.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        private Customer customer;

        //private members and Properties
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }

            set
            {
                this._name = value;
                this.RaisePropertyChanged();
            }
        }

        private string _address;
        public string Address
        {
            get
            {
                return this._address;
            }

            set
            {
                this._address = value;
                this.RaisePropertyChanged();
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return this._city;
            }

            set
            {
                this._city = value;
                this.RaisePropertyChanged();
            }
        }

        private string _zipCode;
        public string ZipCode
        {
            get
            {
                return this._zipCode;
            }

            set
            {
                this._zipCode = value;
                this.RaisePropertyChanged();
            }
        }

        //button commands
        public ICommand AcceptCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private ObservableCollection<State> _states = new ObservableCollection<State>();
        public ObservableCollection<State> States
        {
            get { return _states; }
            set
            {
                _states = value;
                this.RaisePropertyChanged();
            }
        }

        private State _selectedState = new State();
        public State SelectedState
        {
            get
            {
                return this._selectedState;
            }

            set
            {
                this._selectedState = value;
                this.RaisePropertyChanged();
            }
        }

        public AddViewModel()
        {
            //close window command
            CancelCommand = new RelayCommand<Window>((window) =>
            {
                if (window != null)
                {
                    ClearControls();
                    window.Close();
                }
            });

            try
            {
                // Code a query to retrieve the required information from
                // the States table, and sort the results by state name.
                // Bind the State combo box to the query results.
                var states = (from state in MMABooksEntity.mmaBooks.States orderby state.StateName select state).ToList();
                States = new ObservableCollection<State>(states);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

            //accept command that store the information to database and pass back to mainview
            AcceptCommand = new RelayCommand<Window>((window) =>
            {
                try
                {
                    //Check customer information before adding
                    if (IsValidData())
                    {
                        //create a new customer, populate, and add to db
                        customer = new Customer();
                        this.PutCustomerData(customer);
                        MMABooksEntity.mmaBooks.Customers.Add(customer);
                        Console.WriteLine("Database name: " + MMABooksEntity.mmaBooks.Entry(customer).State);
                        MMABooksEntity.mmaBooks.SaveChanges();

                        ClearControls();
                        window.Close();

                        //notify user and send the customer back to MainViewModel for display
                        Messenger.Default.Send(new NotificationMessage("Customer Added!"));
                        Messenger.Default.Send(customer, "add");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }

            });
        }

        //clear all the input from previous dialog
        private void ClearControls()
        {
            Name = null;
            Address = null;
            City = null;
            ZipCode = null;
            SelectedState = null;
        }

        //put the customer information into customer class
        private void PutCustomerData(Customer customer)
        {
            //MMABooksEntity.mmaBooks.Entry(customer).Reference("State1").Load();
            customer.Name = Name;
            customer.Address = Address;
            customer.City = City;
            customer.State = SelectedState.StateCode;
            customer.State1 = SelectedState;
            customer.ZipCode = ZipCode;
        }

        //check for the completion and format of data input
        private bool IsValidData()
        {
            return Validator.IsPresent(Name) &&
                    Validator.IsPresent(Address) &&
                    Validator.IsPresent(City) &&
                    Validator.IsPresent(SelectedState.StateName) &&
                    Validator.IsPresent(ZipCode) &&
                    Validator.IsInt32(ZipCode);
        }
    }
}
