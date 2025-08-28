using System.Text.Json;
using StockQuoteCheckerWebApi.DTOs;
using StockQuoteCheckerWebApi.Interfaces;
using StockQuoteCheckerWebApi.Models.YahooFinance;

namespace StockQuoteCheckerWebApi.Services;


public class YahooFinanceService : IStockQuoteService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<YahooFinanceService> _logger;

    public YahooFinanceService(HttpClient httpClient, ILogger<YahooFinanceService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<StockQuoteDto?> GetQuoteAsync(string ticker)
    {
        try
        {
            var requestUrl = $"v8/finance/chart/{ticker}";
            _logger.LogInformation("Requisitando URL da API externa: {Url}", _httpClient.BaseAddress + requestUrl);

            var response = await _httpClient.GetAsync(requestUrl);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Falha na API do Yahoo para {Ticker}. Status: {StatusCode}. Conteúdo: {Content}",
               ticker, response.StatusCode, content);
                return null;
            }



            var result = JsonSerializer.Deserialize<YahooFinanceResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var quote = result?.Chart?.Result?.FirstOrDefault()?.Meta;

            if (quote == null)
            {
                _logger.LogWarning("-X- Não foi possível obter a cotação do Yahoo Finance.");
                return null;
            }

            return new StockQuoteDto
            {
                Symbol = quote.Symbol,
                RegularMarketPrice = quote.RegularMarketPrice
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao buscar cotação para {Ticker}", ticker);
            return null;
        }
    }
}