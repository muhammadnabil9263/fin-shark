using api.Data;
using api.DTOs;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;

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

        // GET: api/stocks
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList();
            var stockDTOs = stocks.Select(stock => StockMapper.ToDTO(stock)).ToList();
            return Ok(stockDTOs);
        }

        // GET: api/stocks/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound(new { message = $"Stock with id {id} not found." });
            }

            var stockDTO = StockMapper.ToDTO(stock);
            return Ok(stockDTO);
        }

        // POST: api/stocks
        [HttpPost]
        public IActionResult Post([FromBody] StockDTO stockDTO)
        {
            if (stockDTO == null)
            {
                return BadRequest(new { message = "Invalid stock data." });
            }

            var stock = StockMapper.ToModel(stockDTO);
            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return Ok(new { message = "Stock created successfully.",stock = stockDTO });
        }

        // PUT: api/stocks/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StockDTO stockDTO)
        {
            if (stockDTO == null || stockDTO.Id != id)
            {
                return BadRequest();
            }

            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound(new { message = $"Stock with id {id} not found." });
            }

            // Update stock properties
            stock.Symbol = stockDTO.Symbol;
            stock.CompanyName = stockDTO.CompanyName;
            stock.Purchase = stockDTO.Purchase;
            stock.LastDiv = stockDTO.LastDiv;
            stock.Industry = stockDTO.Industry;
            stock.MarketCap = stockDTO.MarketCap;

            _context.Stocks.Update(stock);
            _context.SaveChanges();

            return Ok(new { message = "Stock updated successfully.", stock = stockDTO });
        }

        // DELETE: api/stocks/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound(new { message = $"Stock with id {id} not found." });
            }

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return Ok(new { message = "Stock deleted successfully." });
        }
    }
}