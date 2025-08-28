using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockQuoteCheckerWebApi.Entities;

public class StockPriceHistory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Ticker { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public DateTime Timestamp { get; set; }
}
