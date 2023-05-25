using CatalogAPI.Models;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repository;

public interface ICategoryRepository: IRepository<Category>{

  PagedList<Category> GetCategories(CategoriesParameters categoriesParameters);
  IEnumerable<Category> GetCategoryProducts();
}