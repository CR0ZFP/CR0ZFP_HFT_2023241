using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR0ZFP_HFT_202324.Models;

namespace CR0ZFP_HFT_202324.Repository
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(WebshopDbContext ctx): base(ctx)
        { }
        public override Order Read(int id)
        {
            return this.ctx.Orders.First(t => t.OrderID == id);
        }

        public override void Update(Order item)
        {
            var oldOrder = Read(item.OrderID);
            foreach (var prop in oldOrder.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(oldOrder, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
