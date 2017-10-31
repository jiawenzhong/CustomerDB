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
    public class EditViewModel : ViewModelBase
    {
        public Customer EditCustomer;


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

        private State _selectedState;
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

        public EditViewModel()
        {
            //put the states in the combo box
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

            //register for the messenger sent by MainView 
            Messenger.Default.Register<Customer>(this, "CustomerToEdit", (customer) =>
            {
                try
                {
                    // Code a query to retrieve the selected customer
                    // and store the Customer object in the class variable.
                    EditCustomer = customer;
                    //display the customer in the Edit view screeen
                    this.DisplayCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            });

            //cancel window command
            CancelCommand = new RelayCommand<Window>((window) =>
            {
                if (window != null)
                {
                    window.Close();
                }
            });

            //accept command that store the changes in the database
            AcceptCommand = new RelayCommand<Window>((window) =>
            {
                //Check customer information before updating
                if (IsValidData())
                {
                    //update the customer 
                    PutCustomerData();
                    try
                    {
                        // Update the database.
                        MMABooksEntity.mmaBooks.SaveChanges();

                        //close the window
                        window.Close();

                        //Notify user and send customer back to MainViewModel for display
                        Messenger.Default.Send(new NotificationMessage("Changes Saved!"));
                        Messenger.Default.Send(EditCustomer, "edit");

                    }
                    // Add concurrency error handling.
                    // Place the catch block before the one for a generic exception.
                    catch (DbUpdateConcurrencyException ex)
                    {
                        ex.Entries.Single().Reload();
                        if (MMABooksEntity.mmaBooks.Entry(EditCustomer).State == EntityState.Detached)
                        {

                            MessageBox.Show("Another user has deleted " + "that customer.", "Concurrency Error");
                            window.Close();
                            Messenger.Default.Send(EditCustomer, "clear controls");
                        }
                        else
                        {
                            MessageBox.Show("Another user has updated " + EditCustomer.CustomerID , "Concurrency Error");
                            window.Close();
                            Messenger.Default.Send(EditCustomer, "refresh");
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Incomplete / Invalid Data");
                }
            });

        }

        //update the information in the database
        public void PutCustomerData()
        {
            //get the customer from database 
           // Customer UpdateCustomer;// = (from cust in MMABooksEntity.mmaBooks.Customers where cust.CustomerID == EditCustomer.CustomerID select cust).Single();
            //MMABooksEntity.mmaBooks.Entry(UpdateCustomer).Reference("State1").Load();
            //update the customer
            EditCustomer.Name = Name;
            EditCustomer.Address = Address;
            EditCustomer.City = City;
            EditCustomer.State1 = SelectedState;
            EditCustomer.ZipCode = ZipCode;
            //mark entity as modified
            MMABooksEntity.mmaBooks.Entry(EditCustomer).State = System.Data.EntityState.Modified;
        }

        //display on the screen
        private void DisplayCustomer()
        {
            Name = EditCustomer.Name;
            Address = EditCustomer.Address;
            City = EditCustomer.City;
            //find the customer's state in the state list
            foreach (State s in this.States)
            {
                if (s.StateCode == EditCustomer.State)
                    this.SelectedState = s;
            }
            ZipCode = EditCustomer.ZipCode;
        }

        //check for completion of data input
        private bool IsValidData()
        {
            return Name != "" &&
                    Address != "" &&
                    City != "" &&
                    Validator.IsPresent(SelectedState.StateName) &&
                    ZipCode != "" &&
                    Validator.IsInt32(ZipCode);
        }
    }
}
