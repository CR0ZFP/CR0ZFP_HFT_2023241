using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_202324.Repository;
using CR0ZFP_HFT_2023241.Logic.Interfaces;

namespace CR0ZFP_HFT_2023241.Logic.Classes
{
    public class ProductLogic : IProductLogic
    {
        public IRepository<Product> repository;

        public ProductLogic(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public void Create(Product item)
        {
            if (item.ProductName is null)
            {
                throw new ArgumentException("Must have a name");
            }
            repository.Create(item);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Product Read(int id)
        {
            var product = repository.Read(id);
            if (product == null)
            {
                throw new ArgumentException("This product does not exist");
            }
            return repository.Read(id);
        }

        public IQueryable<Product> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Product item)
        {
            repository.Update(item);
        }

        public double GetAvaragePrice()
        {
            return repository.ReadAll().Average(t => t.Price);
        }

    }
}
