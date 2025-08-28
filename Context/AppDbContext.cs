using Microsoft.EntityFrameworkCore;
using StockQuoteCheckerWebApi.Entities;

namespace StockQuoteCheckerWebApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<StockPriceHistory> StockPriceHistories { get; set; }

}
