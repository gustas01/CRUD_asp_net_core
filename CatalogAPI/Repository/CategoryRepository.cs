using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repository;

public class CategoryRepository: Repository<Category>, ICategoryRepository {
  public CategoryRepository(CatalogAPIContext context) : base(context) { }

  public async Task<PagedList<Category>> GetCategories(CategoriesParameters categoriesParameters)
  {
    return await PagedList<Category>.ToPagedList(Get().OrderBy(ca => ca.Name), categoriesParameters.PageNumber, categoriesParameters.PageSize);
  }

  public async Task<IEnumerable<Category>> GetCategoryProducts(){
    return await Get().Include(x => x.Products).ToListAsync();
  }
}