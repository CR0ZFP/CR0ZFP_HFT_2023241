using System.Collections.Generic;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_2023241.Logic;
using Microsoft.AspNetCore.Mvc;


namespace CR0ZFP_HFT_202324.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderLogic logic;

        public OrderController(IOrderLogic logic)
        {
            this.logic = logic;
        }

        // GET: 
        [HttpGet]
        public IEnumerable<Order> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET 
        [HttpGet("{id}")]
        public Order Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST 
        [HttpPost]
        public void Create([FromBody] Order value)
        {
            this.logic.Create(value);
        }

        // PUT 
        [HttpPut]
        public void Update([FromBody] Order value)
        {
            this.Update(value);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
