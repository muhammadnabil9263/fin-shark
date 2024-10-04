using api.DTOs;
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

    public static Stock ToModel(StockDTO stockDTO)
    {
        return new Stock
        {
            Id = stockDTO.Id,
            Symbol = stockDTO.Symbol,
            CompanyName = stockDTO.CompanyName,
            Purchase = stockDTO.Purchase,
            LastDiv = stockDTO.LastDiv,
            Industry = stockDTO.Industry,
            MarketCap = stockDTO.MarketCap
        };
    }
}
