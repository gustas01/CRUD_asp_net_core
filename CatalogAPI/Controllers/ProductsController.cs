using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Models;
using CatalogAPI.Repository;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;

  public ProductsController(IUnitOfWork context)
  {
    _unitOfWork = context;
  }


  [HttpGet("orderByPrice")]
  public ActionResult<IEnumerable<Product>> GetProductsByPrice(){
    return _unitOfWork.ProductRepository.GetProductsByPrice().ToList();
  }

  [HttpGet]
  public ActionResult<IEnumerable<Product>> Get(){
    try
    {
      List<Product> products = _unitOfWork.ProductRepository.Get().ToList();
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
      var product = _unitOfWork?.ProductRepository.GetById(p => p.ProductId == id);
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
      _unitOfWork?.ProductRepository?.Add(product);
      _unitOfWork?.Commit();
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

      _unitOfWork.ProductRepository.Update(product);
      _unitOfWork.Commit();

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
      Product product = _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
      if(product is null) return NotFound("Produto não encontrado");

      _unitOfWork.ProductRepository.Delete(product);
      _unitOfWork.Commit();

      return Ok(product);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}