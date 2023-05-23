using CatalogAPI.Models;

namespace CatalogAPI.Repository;

public interface ICategoryRepository: IRepository<Category>{

  IEnumerable<Category> GetCategoryProducts();
}