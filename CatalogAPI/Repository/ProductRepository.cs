using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
  public ProductRepository(CatalogAPIContext context) : base(context) { }

  public async Task<PagedList<Product>> GetProducts(ProductsParameters productsParameters)
  {
    // return Get().OrderBy(pr => pr.Name)
    //             .Skip((productsParameters.PageNumber - 1) * productsParameters.PageSize)
    //             .Take(productsParameters.PageSize)
    //             .ToList();

    return await PagedList<Product>.ToPagedList(Get().OrderBy(pr => pr.Name), productsParameters.PageNumber, productsParameters.PageSize);
  }

  public async Task<IEnumerable<Product>> GetProductsByPrice(){
    return await Get().OrderBy(c => c.Price).ToListAsync();
  }
}