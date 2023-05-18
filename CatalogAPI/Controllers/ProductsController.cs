using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

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
    try
    {
      List<Product> products = _context.Products.AsNoTracking().ToList();
      if(products is null) return NotFound("Produtos não encontrados");
      return products;
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }

  [HttpGet("{id:int}", Name="ReadProduct")]
  public ActionResult<Product> Get(int id){
    try
    {
      var product = _context?.Products?.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
      if(product is null) return NotFound("Produto não encontrado");
      return product;
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPost]
  public ActionResult Post(Product product){
    try
    {
      if(product is null) return BadRequest();
      _context?.Products?.Add(product);
      _context?.SaveChanges();
      return new CreatedAtRouteResult("ReadProduct", new { id = product.ProductId }, product);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }

  [HttpPut("{id:int}")]
  public ActionResult Put(int id, Product product){
    try
    {
      if(id != product.ProductId) return BadRequest();

      _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      _context.SaveChanges();

      return Ok(product);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }

  [HttpDelete("{id:int}")]
  public ActionResult Delete(int id){
    try
    {
      Product product = _context.Products.FirstOrDefault(p => p.ProductId == id);
      if(product is null) return NotFound("Produto não encontrado");

      _context.Products.Remove(product);
      _context.SaveChanges();

      return Ok(product);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}