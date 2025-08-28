using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockQuoteCheckerWebApi.Context;
using StockQuoteCheckerWebApi.DTOs;
using StockQuoteCheckerWebApi.Entities;
using StockQuoteCheckerWebApi.Interfaces;

namespace StockQuoteCheckerWebApi.Controllers;

[Route("api/stocks")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockQuoteService _stockQuoteService;
    private readonly AppDbContext _context;

    public StockController(IStockQuoteService stockQuoteService, AppDbContext context)
    {
        _stockQuoteService = stockQuoteService;
        _context = context;
    }


    /// <summary>
    /// Busca a cotação mais recente de um ativo.
    /// </summary>
    /// <remarks>
    /// Esse endpoint consulta a API do Yahoo Finance em tempo real, persiste o resultado
    /// no banco de dados para histórico e retorna a cotação atual.
    /// </remarks>
    /// <param name="ticker">O código do ativo na B3 (por exemplo: PETR4.SA, MGLU3.SA)</param>
    /// <returns>Um objeto com os detalhes da cotação do ativo.</returns>
    [HttpGet("{ticker}")] // exemplo: GET api/stocks/PETR4.SA
    [ProducesResponseType(typeof(StockQuoteDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetStockPrice(string ticker)
    {
        if (string.IsNullOrWhiteSpace(ticker))
        {
            return BadRequest("O ticker não pode ser vazio.");
        }

        var quote = await _stockQuoteService.GetQuoteAsync(ticker);

        if (quote == null)
        {
            return NotFound($"Não foi possível encontrar a cotação para o ticker {ticker}.");
        }

        var historyEntry = new StockPriceHistory
        {
            Ticker = quote.Symbol,
            Price = quote.RegularMarketPrice,
            Timestamp = DateTime.UtcNow
        };

        _context.StockPriceHistories.Add(historyEntry);

        await _context.SaveChangesAsync();



        return Ok(quote);
    }


    /// <summary>
    /// Obtém o histórico de cotações salvas para um ativo.
    /// </summary>
    /// <param name="ticker">O código do ativo a ser consultado (por exemplo: PETR4.SA)</param>
    /// <returns>Uma lista do histórico dos preços do ativo.</returns>
    /// <response code="200">Retorna a lista de cotações salvas.</response>
    [HttpGet("{ticker}/history")]
    [ProducesResponseType(typeof(IEnumerable<StockPriceHistory>), 200)]
    public async Task<IActionResult> GetStockHistory(string ticker)
    {
        var history = await _context.StockPriceHistories
                                    .Where(h => h.Ticker.ToUpper() == ticker.ToUpper())
                                    .OrderByDescending(h => h.Timestamp)
                                    .ToListAsync();

        return Ok(history);
    }

}
