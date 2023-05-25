using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repository;

public class CategoryRepository: Repository<Category>, ICategoryRepository {
  public CategoryRepository(CatalogAPIContext context) : base(context) { }

  public PagedList<Category> GetCategories(CategoriesParameters categoriesParameters)
  {
    return PagedList<Category>.ToPagedList(Get().OrderBy(ca => ca.Name), categoriesParameters.PageNumber, categoriesParameters.PageSize);
  }

  public IEnumerable<Category> GetCategoryProducts(){
    return Get().Include(x => x.Products);
  }
}