using CR0ZFP_HFT_202324.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Xps.Serialization;

namespace WebshopDBApp.WpfClient
{
    public class CustomerWindowViewModell :ObservableRecipient
    {
        public RestCollection<Customer> Customers { get; set; }
        public ICommand CreateCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }

        private Customer selectedCustomer;

        public Customer  SelectedCustomer
        {
            get { return selectedCustomer; }
            set {
                if (value != null)
                {
                    selectedCustomer = new Customer()
                    {
                        CustomerName = value.CustomerName,
                        CustomerID = value.CustomerID,
                        Address = value.Address,
                        Email = value.Email,
                        Age = value.Age,
                    };
                    OnPropertyChanged();
                    (DeleteCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public CustomerWindowViewModell()
        {
            if (!IsInDesignMode)
            {
                Customers = new RestCollection<Customer>("http://localhost:34970/", "customer");

                CreateCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Add(new Customer()
                    {
                        CustomerName = SelectedCustomer.CustomerName,
                        Address = SelectedCustomer.Address,
                        Age = SelectedCustomer.Age,
                        Email = SelectedCustomer.Email,
                    });
                });

                UpdateCustomerCommand = new RelayCommand (()=>
                {
                    try
                    {
                        Customers.Update(SelectedCustomer);

                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                );

                DeleteCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Delete(SelectedCustomer.CustomerID);


                }, () =>
                {
                    return selectedCustomer != null;
                });

                selectedCustomer = new Customer();



            }
        }
    }
    
}
