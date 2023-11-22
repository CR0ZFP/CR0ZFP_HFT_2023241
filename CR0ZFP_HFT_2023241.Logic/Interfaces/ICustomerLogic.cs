using System.Collections.Generic;
using System.Linq;
using CR0ZFP_HFT_202324.Models;

namespace CR0ZFP_HFT_2023241.Logic.Interfaces
{
    public interface ICustomerLogic
    {
        double AvarageAgeOfCustomers();
        void Create(Customer item);
        void Delete(int id);
        List<Customer> GetCustomersByCounty(string County);
        Customer Read(int id);
        IQueryable<Customer> ReadAll();
        void Update(Customer item);
    }
}