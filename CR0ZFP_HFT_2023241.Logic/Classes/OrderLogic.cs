using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_202324.Repository;
using CR0ZFP_HFT_2023241.Logic;

namespace CR0ZFP_HFT_2023241.Logic
{
    public class OrderLogic : IOrderLogic
    {
        public IRepository<Order> repository;


        public OrderLogic(IRepository<Order> repository)
        {
            this.repository = repository;
        }

        public void Create(Order item)
        {
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Order Read(int id)
        {
            var order = repository.Read(id);
            if (order is null)
            {
                throw new ArgumentException("This order does not exist");
            }
            return repository.Read(id);
        }

        public IQueryable<Order> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Order item)
        {
            repository.Update(item);
        }


        //Option(1):by exact date
        //Option(2): from a date
        public List<Order> GetOrdersByDate(DateTime Date, int option)
        {
            if (option == 1)
            {
                return repository.ReadAll().Where(t => t.OrderDate.Equals(Date)).ToList();
            }
            else if (option == 2)
            {
                return repository.ReadAll().Where(t => t.OrderDate > Date).ToList();
            }
            else { throw new ArgumentException("Theres no such option"); }
        }

        public double GetFullPrice(int Id)
        {
            double price = 0;
            foreach (Product products in repository.Read(Id).Products)
            {
                price += products.Price;
            }

            return price;
        }

        //public double GetFullWeight(int Id)
        //{
        //    double weight = 0;
        //    foreach (Product products in repository.Read(Id).Products)
        //    {
        //        weight += products.Weight;
        //    }
        //    return weight;
        //}


    }
    }

