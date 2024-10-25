using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers;

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
    public async Task<IActionResult> GetAll([FromQuery] QueryObject quary)
    {
        var stocks = await _stockRepository.GetAllAsync(quary);
        var stockDTOs = stocks.Select(stock => StockMapper.ToDTO(stock)).ToList();
        return Ok(stockDTOs);
    }


    // GET: api/stocks/5
    [HttpGet("{id}")]
    [Authorize]
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

        // Optional: Check if the model state is valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // This will return model validation errors
        }

        Stock stock = StockMapper.ToStockModelFromCreateDTO(stockDTO);

        await _stockRepository.CreateAsync(stock);

        // Return 201 Created status with the created stock object and location
        return CreatedAtAction(nameof(GetById), new { id = stock.Id }, new { message = "Stock created successfully.", stock });
    }



    //PUT: api/stocks/5

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateStockDTO stockDTO)
    {
        if (stockDTO == null)
        {
            return BadRequest(new { message = "Invalid stock data." });
        }

        // Map the DTO to the stock model
        var stock = StockMapper.ToStockModelFromUpdatedDTO(stockDTO);

        // Call the repository to update the stock
        var result = await _stockRepository.UpdateAsync(id, stock);

        if (result)
        {
            return Ok(new { message = "Stock updated successfully.", stock = stockDTO });
        }

        // Stock not found or update failed
        return NotFound(new { message = $"Stock with ID {id} not found." });
    }



    // DELETE: api/stocks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _stockRepository.DeleteAsync(id);

        if (result)
        {
            return Ok(new { message = "Stock deleted successfully." });
        }
        return NotFound();

    }

}
