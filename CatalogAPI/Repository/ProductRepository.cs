using CatalogAPI.Context;
using CatalogAPI.Models;

namespace CatalogAPI.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
  public ProductRepository(CatalogAPIContext context) : base(context) { }

  public IEnumerable<Product> GetProductsByPrice(){
    return Get().OrderBy(c => c.Price).ToList();
  }
}