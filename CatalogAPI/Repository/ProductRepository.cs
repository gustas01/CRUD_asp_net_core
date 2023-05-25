using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Pagination;

namespace CatalogAPI.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
  public ProductRepository(CatalogAPIContext context) : base(context) { }

  public PagedList<Product> GetProducts(ProductsParameters productsParameters)
  {
    // return Get().OrderBy(pr => pr.Name)
    //             .Skip((productsParameters.PageNumber - 1) * productsParameters.PageSize)
    //             .Take(productsParameters.PageSize)
    //             .ToList();

    return PagedList<Product>.ToPagedList(Get().OrderBy(pr => pr.Name), productsParameters.PageNumber, productsParameters.PageSize);
  }

  public IEnumerable<Product> GetProductsByPrice(){
    return Get().OrderBy(c => c.Price).ToList();
  }
}