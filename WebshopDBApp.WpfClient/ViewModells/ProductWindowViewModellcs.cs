using CR0ZFP_HFT_202324.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WebshopDBApp.WpfClient.ViewModells;

namespace WebshopDBApp.WpfClient
{
    public class ProductWindowViewModell : ObservableRecipient
    {
        public RestCollection<Product> Products { get; set; }
        public ICommand CreateProductCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                if (value != null)
                {
                    selectedProduct = new Product()
                    {
                        ProductName = value.ProductName,
                        OrderID = value.OrderID,
                        Weight = value.Weight,
                        Price = value.Price,
                    };
                    OnPropertyChanged();
                    (DeleteProductCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public ProductWindowViewModell()
        {
            if (!IsInDesignMode)
            {
                Products = new RestCollection<Product>("http://localhost:34970/", "product", "hub");

                CreateProductCommand = new RelayCommand(() =>
                {
                    Products.Add(new Product()
                    {
                        ProductName = SelectedProduct.ProductName,
                        OrderID = SelectedProduct.OrderID,
                        Weight = SelectedProduct.Weight,
                        Price = SelectedProduct.Price,
                    });
                });

                UpdateProductCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Products.Update(SelectedProduct);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                );

                DeleteProductCommand = new RelayCommand(() =>
                {
                    Products.Delete(SelectedProduct.ProductID);


                }, () =>
                {
                    return selectedProduct != null;
                });

                selectedProduct = new Product();



            }
        }
    }
}
