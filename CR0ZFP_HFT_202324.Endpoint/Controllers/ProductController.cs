using System.Collections.Generic;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_2023241.Logic;
using Microsoft.AspNetCore.Mvc;


namespace CR0ZFP_HFT_202324.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PoductController : ControllerBase
    {
        IProductLogic logic;

        public PoductController(IProductLogic logic)
        {
            this.logic = logic;
        }

        // GET: 
        [HttpGet]
        public IEnumerable<Product> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET 
        [HttpGet("{id}")]
        public Product Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST 
        [HttpPost]
        public void Create([FromBody] Product value)
        {
            this.logic.Create(value);
        }

        // PUT 
        [HttpPut]
        public void Update([FromBody] Product value)
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
