using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository

    {

        private readonly ApplicationDbContext _context;


        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task< Stock>CreateAsync(Stock  stock)
        {

            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }


        public async Task<bool> UpdateAsync(int id, Stock updatedStock)
        {

            var existingStock = await _context.Stocks.FindAsync(id);

            if (existingStock == null)
            {
                return false; // Stock not found
            }

            existingStock.Symbol = updatedStock.Symbol;
            existingStock.CompanyName = updatedStock.CompanyName;
            existingStock.Purchase = updatedStock.Purchase;
            existingStock.LastDiv = updatedStock.LastDiv;
            existingStock.Industry = updatedStock.Industry;
            existingStock.MarketCap = updatedStock.MarketCap;

            _context.Stocks.Update(existingStock);
            await _context.SaveChangesAsync();

            return true; // Update successful
        }


        public async Task<bool> DeleteAsync(int id)
        {
            // Find the stock by ID
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                // Return false if the stock was not found
                return false;
            }
            // Remove the stock from the context
            _context.Stocks.Remove(stock);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return true indicating the stock was successfully deleted
            return true;
        }

        public Task<bool> StockExistsAsync(int id)
        {

            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        
    }

}
