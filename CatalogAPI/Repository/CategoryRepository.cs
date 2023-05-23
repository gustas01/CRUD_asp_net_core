using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repository;

public class CategoryRepository: Repository<Category>, ICategoryRepository {
  public CategoryRepository(CatalogAPIContext context) : base(context) { }

  public IEnumerable<Category> GetCategoryProducts(){
    return Get().Include(x => x.Products);
  }
}