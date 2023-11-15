using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CR0ZFP_HFT_202324.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected WebshopDbContext ctx;

        public Repository(WebshopDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }
        public abstract T Read(int id);

        public abstract void Update(T item);

        public void Delete(int id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

       
    }
}
