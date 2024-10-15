using api.Data;
using api.DTOs;
using api.Interfaces;
using api.Mappers;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StocksController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }


        // GET: api/stocks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var stockDTOs = stocks.Select(stock => StockMapper.ToDTO(stock)).ToList();
            return Ok(stockDTOs);
        }


        // GET: api/stocks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound(new { message = $"Stock with id {id} not found." });
            }

            var stockDTO = StockMapper.ToDTO(stock);
            return Ok(stockDTO);
        }


        // POST: api/stocks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStockDTO stockDTO)
        {
            if (stockDTO == null)
            {
                return BadRequest(new { message = "Invalid stock data." });
            }

            await _stockRepository.CreateAsync(stockDTO);

            return Ok(new { message = "Stock created successfully.", stock = stockDTO });
        }
        
        
        //PUT: api/stocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateStockDTO stockDTO)
        {
         
        
           var result = await _stockRepository.UpdateAsync(id,stockDTO);
            
            if (result)
            {
                return Ok(new { message = "Stock updated successfully.", stock = stockDTO });
            }

            return NotFound();
        }
        
        
        // DELETE: api/stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stockModel = await _stockRepository.DeleteAsync(id);

            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Stock deleted successfully." });
        }

    }
}
