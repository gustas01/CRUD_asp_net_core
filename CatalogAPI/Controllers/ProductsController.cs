using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Context;
using CatalogAPI.Models;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
  private readonly CatalogAPIContext? _context;

  public ProductsController(CatalogAPIContext context)
  {
    _context = context;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Product>> Get(){
    var products = _context.Products.ToList();
    if(products is null) return NotFound("Produtos não encontrados");
    return products;
  }
}