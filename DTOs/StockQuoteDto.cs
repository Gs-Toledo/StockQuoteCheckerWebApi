namespace StockQuoteCheckerWebApi.DTOs;

public class StockQuoteDto
{
    /// <summary>
    /// O código (ticker) do ativo.
    /// </summary>
    /// <example>PETR4.SA</example>
    public string Symbol { get; set; }

    /// <summary>
    /// O preço de mercado atual do ativo.
    /// </summary>
    /// <example>40.25</example>
    public decimal RegularMarketPrice { get; set; }

    /// <summary>
    /// O timestamp de registro do ativo.
    /// </summary>
    /// <example>2025-08-25T13:00:00.1234567Z</example>
    public long RegularMarketTime { get; set; }
}