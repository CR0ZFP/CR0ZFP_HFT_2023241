using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR0ZFP_HFT_202324.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CR0ZFP_HFT_202324.Repository
{
    public class CustomerRepository : Repository<Customer>, IRepository<Customer>
    {
        public CustomerRepository(WebshopDbContext ctx) : base(ctx) 
        { }
        
        public override Customer Read(int id)
        {
            return this.ctx.Customers.First(t => t.CustomerID == id);
        }

        public override void Update(Customer item)
        {
            var oldCustomer = Read(item.CustomerID);
            foreach(var prop in oldCustomer.GetType().GetProperties())
            {
                prop.SetValue(oldCustomer, prop.GetValue(item));
            }

        }
    }
}
