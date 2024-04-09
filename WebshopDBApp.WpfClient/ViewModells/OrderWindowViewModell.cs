using CR0ZFP_HFT_202324.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WebshopDBApp.WpfClient.ViewModells;

namespace WebshopDBApp.WpfClient
{
    class OrderWindowViewModell : ObservableRecipient
    {
        public RestCollection<Order> Orders { get; set; }
        public ICommand CreateOrderCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (value != null)
                {
                    selectedOrder = new Order()
                    {
                        OrderID = value.OrderID,
                        CustomerID = value.CustomerID,
                        OrderDate = value.OrderDate,
                        
                    };
                    OnPropertyChanged();
                    (DeleteOrderCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public OrderWindowViewModell()
        {
            if (!IsInDesignMode)
            {
                Orders = new RestCollection<Order>("http://localhost:34970/", "order", "hub");

                CreateOrderCommand = new RelayCommand(() =>
                {
                    Orders.Add(new Order()
                    {
                        OrderID = SelectedOrder.OrderID,
                         CustomerID= SelectedOrder.CustomerID,
                        OrderDate = SelectedOrder.OrderDate,
                        
                    });
                });

                UpdateOrderCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Orders.Update(SelectedOrder);

                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                );

                DeleteOrderCommand = new RelayCommand(() =>
                {
                    Orders.Delete(SelectedOrder.OrderID);


                }, () =>
                {
                    return selectedOrder != null;
                });

                selectedOrder = new Order();



            }
        }
    }
}
