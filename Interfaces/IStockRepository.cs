using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{

    Task<List<Stock>> GetAllAsync();

    Task CreateAsync(CreateStockDTO stock);

    Task<Stock> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id , UpdateStockDTO stockDto);

    Task<Stock> DeleteAsync(int id);

}

