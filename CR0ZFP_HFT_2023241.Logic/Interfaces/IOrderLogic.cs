using System;
using System.Collections.Generic;
using System.Linq;
using CR0ZFP_HFT_202324.Models;

namespace CR0ZFP_HFT_2023241.Logic
{
    public interface IOrderLogic
    {
        void Create(Order item);
        void Delete(int id);
        List<Order> GetOrdersByDate(DateTime Date, int option);
        //double GetFullPrice(int Id);
        //double GetFullWeight(int Id);
        Order Read(int id);
        IQueryable<Order> ReadAll();
        void Update(Order item);
    }
}