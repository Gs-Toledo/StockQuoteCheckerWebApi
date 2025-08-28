namespace StockQuoteCheckerWebApi.Models.YahooFinance;

public class YahooChart
{
    public List<YahooResult> Result { get; set; }
    public object Error { get; set; }
}
