using CatalogAPI.Models;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repository;

public interface IProductRepository : IRepository<Product>{
  PagedList<Product> GetProducts(ProductsParameters productsParameters);
  IEnumerable<Product> GetProductsByPrice();

}