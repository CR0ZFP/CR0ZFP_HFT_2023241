using System.Linq;
using CR0ZFP_HFT_202324.Models;

namespace CR0ZFP_HFT_2023241.Logic.Interfaces
{
    public interface IProductLogic
    {
        void Create(Product item);
        void Delete(int id);
        double GetAvaragePrice();
        Product Read(int id);
        IQueryable<Product> ReadAll();
        void Update(Product item);
    }
}