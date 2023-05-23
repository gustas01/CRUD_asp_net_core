using CatalogAPI.Models;

namespace CatalogAPI.Repository;

public interface IProductRepository : IRepository<Product>{
  IEnumerable<Product> GetProductsByPrice();
}