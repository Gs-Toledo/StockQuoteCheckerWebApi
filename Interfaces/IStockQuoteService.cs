using StockQuoteCheckerWebApi.DTOs;

namespace StockQuoteCheckerWebApi.Interfaces;

public interface IStockQuoteService
{
    Task<StockQuoteDto?> GetQuoteAsync(string ticker);
}
