using CatalogAPI.Models;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repository;

public interface ICategoryRepository: IRepository<Category>{

  Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters);
  Task<IEnumerable<Category>> GetCategoryProducts();
}