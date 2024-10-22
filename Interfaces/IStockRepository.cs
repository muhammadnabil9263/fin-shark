using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;
public interface IStockRepository
{
    // Get all stock records
    Task<List<Stock>> GetAllAsync();

    // Create a new stock entry
    Task<Stock> CreateAsync(Stock stock);

   // Get a specific stock by ID
    Task<Stock?> GetByIdAsync(int id);

    // Update an existing stock record
    Task<bool> UpdateAsync(int id, Stock stock);

    // Delete a stock record
    Task<bool> DeleteAsync(int id);

    // Check if stock exists by ID
    Task<bool> StockExistsAsync(int id);
}
