using System.Collections.Generic;
using CR0ZFP_HFT_202324.Endpoint.Services;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_2023241.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;


namespace CR0ZFP_HFT_202324.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerLogic logic;
        IHubContext<SignalRHub> hub;


        public CustomerController(ICustomerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: 
        [HttpGet]
        public IEnumerable<Customer> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET 
        [HttpGet("{id}")]
        public Customer Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST 
        [HttpPost]
        public void Create([FromBody] Customer value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CustomerCreated", value);
        }

        // PUT 
        [HttpPut]
        public void Update([FromBody] Customer value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CustomerUpdated", value);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var old = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CustomerDeleted", old);
        }
    }
}
