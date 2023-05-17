using Microsoft.EntityFrameworkCore;
using CatalogAPI.Models;

namespace CatalogAPI.Context;

public class CatalogAPIContext : DbContext {
  public CatalogAPIContext(DbContextOptions<CatalogAPIContext> options) : base(options){}

  public DbSet<Category>? Categories {get; set;}
  public DbSet<Product>? Products {get; set;}
}