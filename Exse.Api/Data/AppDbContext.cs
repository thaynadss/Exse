using System.Reflection;
using Exse.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Exse.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Category> Categories { get; set; } = null!;
  public DbSet<Transaction> Transactions { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}