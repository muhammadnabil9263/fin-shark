using api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<StocksController>
        [HttpGet("getAll")]
        public IActionResult GetAll ()
        {
            var stocks = _context.Stocks.ToList(); 
            return Ok(stocks);
        }


        // GET api/<StocksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StocksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StocksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StocksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
