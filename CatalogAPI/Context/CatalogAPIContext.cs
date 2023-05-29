using Microsoft.EntityFrameworkCore;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CatalogAPI.Context;

public class CatalogAPIContext : IdentityDbContext {
  public CatalogAPIContext(DbContextOptions<CatalogAPIContext> options) : base(options){}

  public DbSet<Category>? Categories {get; set;}
  public DbSet<Product>? Products {get; set;}
}