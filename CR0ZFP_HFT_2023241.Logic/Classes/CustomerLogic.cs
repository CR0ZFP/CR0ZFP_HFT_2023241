using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_202324.Repository;
using CR0ZFP_HFT_2023241.Logic.Interfaces;

namespace CR0ZFP_HFT_2023241.Logic.Classes
{
    public class CustomerLogic : ICustomerLogic
    {
        IRepository<Customer> repository;

        public CustomerLogic(IRepository<Customer> repository)
        {
            this.repository = repository;
        }

        public void Create(Customer item)
        {
            if (item.Age < 18)
            {
                throw new ArgumentException("Buddy you too young for this");
            }
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Customer Read(int id)
        {
            var customer = repository.Read(id);
            if (customer is null)
            {
                throw new ArgumentException("This person do not exist");
            }
            return repository.Read(id);
        }

        public IQueryable<Customer> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Customer item)
        {
            repository.Update(item);
        }

        public double AvarageAgeOfCustomers()
        {
            return repository.ReadAll().Average(x => x.Age);
        }

        public List<Customer> GetCustomersByCounty(string County)
        {
            return repository.ReadAll().Where(t => t.Address.Contains(County)).ToList();
        }



    }
}
