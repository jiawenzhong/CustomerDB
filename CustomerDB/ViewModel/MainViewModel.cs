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
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Customer selectedCustomer;

        // command properties
        public ICommand GetCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        //button visibility
        private bool _deleteEnabled = false;
        public bool DeleteEnabled
        {
            get
            {
                return this._deleteEnabled;
            }

            set

            {
                this._deleteEnabled = value;
                this.RaisePropertyChanged();
            }
        }
        private bool _modifyEnable = false;
        public bool ModifyEnable
        {
            get
            {
                return this._modifyEnable;
            }

            set

            {
                this._modifyEnable = value;
                this.RaisePropertyChanged();
            }
        }

        //private members and view properties
        private string _customerID;
        public string CustomerID
        {
            get
            {
                return this._customerID;
            }

            set

            {
                this._customerID = value;
                this.RaisePropertyChanged();
            }
        }

        private string _stateCode;
        public string StateCode
        {
            get
            {
                return this._stateCode;
            }

            set
            {
                this._stateCode = value;
                this.RaisePropertyChanged();
            }
        }

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
        public MainViewModel()
        {
            Messenger.Default.Register<Customer>(this, "refresh", (customer) =>
            {
                selectedCustomer = customer;
                GetCustomer(selectedCustomer.CustomerID);
            });

            Messenger.Default.Register<Customer>(this, "clear controls", (customer) =>
            {
                ClearControls();
            });

            //Message receivers to accept messages from EditViewModel and AddViewModel
            //The sender also sends a token as the second parameter and only receivers with the same token will accept it.
            Messenger.Default.Register<Customer>(this, "add", (customer) =>
            {
                try
                {
                    // Code a query to retrieve the selected customer
                    // and store the Customer object in the class variable.
                    selectedCustomer = customer;
                    this.DisplayCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            });

            //register for messenger from editView
            Messenger.Default.Register<Customer>(this, "edit", (customer) =>
            {
                try
                {
                    // Code a query to retrieve the selected customer
                    // and store the Customer object in the class variable.
                    selectedCustomer = customer;
                    this.DisplayCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            });

            //open add window 
            AddCommand = new RelayCommand(() =>
            {
                //call and display addview
                AddView addView = new AddView();
                addView.ShowDialog();
            });

            //open edit window and send customer info to edit view
            EditCommand = new RelayCommand(() =>
            {
                //call and display the edit view
                EditView editView = new EditView();
                //send message to edit view before window pops out
                Messenger.Default.Send(selectedCustomer, "CustomerToEdit");
                editView.ShowDialog();

            });

            //get the customer information with customer ID
            GetCommand = new RelayCommand(() =>
            {
                if (Validator.IsInt32(CustomerID) && Validator.IsPresent(CustomerID))
                {
                    int customerID = int.Parse(CustomerID);
                    GetCustomer(customerID);
                }
            });

            //close window command
            ExitCommand = new RelayCommand<Window>((window) =>
            {
                if (window != null)
                {
                    window.Close();
                }
            });

            //command to remove customer
            DeleteCommand = new RelayCommand(() =>
            {
                try
                {
                    // Mark the row for deletion.
                    // Update the database.
                    MMABooksEntity.mmaBooks.Customers.Remove(selectedCustomer);
                    //MMABooksEntity.mmaBooks.Entry(selectedCustomer).State = System.Data.EntityState.Detached;
                    MMABooksEntity.mmaBooks.SaveChanges();
                    Messenger.Default.Send(new NotificationMessage("Customer " + CustomerID + " Removed!"));

                    CustomerID = "";
                    this.ClearControls();
                }
                // Add concurrency error handling.
                // Place the catch block before the one for a generic exception.
                catch (DbUpdateConcurrencyException ex)
                {
                    ex.Entries.Single().Reload();
                    if (MMABooksEntity.mmaBooks.Entry(selectedCustomer).State == EntityState.Detached)
                    {
                        MessageBox.Show("Another user has deleted " + "that customer.", "Concurrency Error");
                        this.ClearControls();
                    }
                    else
                    {
                        MessageBox.Show("Another user has updated " + "that customer.", "Concurrency Error");
                        DisplayCustomer();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            });

        }

        //enable the use of modify button and the delete button
        private void EnableButton()
        {
                ModifyEnable = true;
                DeleteEnabled = true;
        }

        //disable the use of modify button and the delete button
        private void DisableButton()
        {
            ModifyEnable = false;
            DeleteEnabled = false;
        }

        //retrieve the customer by customer ID
        private void GetCustomer(int CustomerID)
        {
            try
            {
                //ClearControls();
                selectedCustomer =
                    (from customer in MMABooksEntity.mmaBooks.Customers
                     where customer.CustomerID == CustomerID
                     select customer).Single();
                //Console.WriteLine("Selected Customer Name: " + selectedCustomer.Name);


                if (selectedCustomer == null)
                {
                    MessageBox.Show("No customer found with this ID. " +
                        "Please try again. ");
                    this.ClearControls();
                    DisableButton();
                }
                else
                {
                    if (!MMABooksEntity.mmaBooks.Entry(selectedCustomer).Reference("State1").IsLoaded)
                    {
                        MMABooksEntity.mmaBooks.Entry(selectedCustomer).Reference("State1").Load();
                    }
                    this.DisplayCustomer();
                    //check if customer info is loaded in the text box
                    EnableButton();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Customer is found with this ID");
                this.ClearControls();
                DisableButton();
            }
        }

        //display the customer on the screen
        private void DisplayCustomer()
        {
            //Console.WriteLine("DisplayCustomer - Selected Customer Name: " + selectedCustomer.Name);
            CustomerID = selectedCustomer.CustomerID.ToString();
            Name = selectedCustomer.Name;
            Address = selectedCustomer.Address;
            City = selectedCustomer.City;
            this.StateCode = selectedCustomer.State1.StateCode;
            ZipCode = selectedCustomer.ZipCode;
            DeleteEnabled = true;
            ModifyEnable = true;

        }

        //clear all the context in the textbox
        private void ClearControls()
        {
            CustomerID = null;
            Name = null;
            Address = null;
            City = null;
            this.StateCode = null;
            ZipCode = null;
        }
    }
}