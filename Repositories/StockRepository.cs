using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Repositories
{
    public class StockRepository : IStockRepository

    {

        private readonly ApplicationDbContext _context;


        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {

                return await stocks.Where(s => s.Symbol == query.Symbol).ToListAsync();
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {

                return await stocks.Where(s => s.CompanyName == query.CompanyName).ToListAsync();
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            return await stocks.ToListAsync();
        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock> CreateAsync(Stock stock)
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
