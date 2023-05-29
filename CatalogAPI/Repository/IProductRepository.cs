using CatalogAPI.Models;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repository;

public interface IProductRepository : IRepository<Product>{
  Task<PagedList<Product>> GetProducts(ProductsParameters productsParameters);
  Task<IEnumerable<Product>> GetProductsByPrice();

}