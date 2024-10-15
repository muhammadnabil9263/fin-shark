using api.DTOs.Stock;
using api.Models;

namespace api.Mappers;

public class StockMapper
{
    public static StockDTO ToDTO(Stock stock)
    {
        return new StockDTO
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
    public static Stock ToStockFromCreateDTO(CreateStockDTO stockDto)
    {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }

}
