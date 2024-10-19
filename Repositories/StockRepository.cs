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

        public async Task CreateAsync(CreateStockDTO stockDto)
        {

            var stock = StockMapper.ToStockModelFromCreateDTO(stockDto);
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int Id, UpdateStockDTO stockDTO)
        {


            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == Id);

            if (stock == null)
            {
                return false; // Stock not found
            }

            stock.Symbol = stockDTO.Symbol;
            stock.CompanyName = stockDTO.CompanyName;
            stock.Purchase = stockDTO.Purchase;
            stock.LastDiv = stockDTO.LastDiv;
            stock.Industry = stockDTO.Industry;
            stock.MarketCap = stockDTO.MarketCap;

            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();

            return true; // Update successful
        }


        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
            return stock; 
        }

        public Task<bool> StockExists(int id) {

            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        
    }

}
