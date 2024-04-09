using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR0ZFP_HFT_202324.Models;

namespace CR0ZFP_HFT_202324.Repository
{
    public class ProductRepository : Repository<Product>, IRepository<Product>
    {
        public ProductRepository(WebshopDbContext ctx): base(ctx) { }
        
        public override Product Read(int id)
        {
            return this.ctx.Products.First(t => t.ProductID == id);
        }

        public override void Update(Product item)
        {
            var oldProduct = Read(item.ProductID);
            foreach (var prop in oldProduct.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(oldProduct, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }

    
    
}
