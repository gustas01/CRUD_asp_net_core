using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Context;
using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly CatalogAPIContext _context;

  public CategoriesController(CatalogAPIContext context)
  {
    _context = context;
  }
  

  [HttpGet]
  public ActionResult<IEnumerable<Category>> Index(){
    try
    {
      List<Category> categories = _context.Categories.AsNoTracking().ToList();
      if(categories is null) return NotFound("Categorias não encontradas");
      return categories;
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpGet("products")]
  public ActionResult<IEnumerable<Category>> GetInnerCategoryProduct(){
    try
    {
      return _context.Categories.Include(p => p.Products).AsNoTracking().ToList();
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpGet("{id:int}", Name="ReadCategory")]
  public ActionResult<Category> Read(int id){
    try
    {
      Category categorie = _context?.Categories?.AsNoTracking().FirstOrDefault(c => c.CategoryId == id);
      if(categorie is null) return NotFound($"Categoria com id={id} não encontrada");
      return categorie;
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPost]
  public ActionResult Create(Category category){
    try
    {
      if(category is null) return BadRequest();
      _context?.Categories?.Add(category);
      _context?.SaveChanges();
      return new CreatedAtRouteResult("ReadCategory", new { id = category.CategoryId }, category);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPut("{id:int}")]
  public ActionResult Update(int id, Category category){
    try
    {
      if(id != category.CategoryId) return BadRequest();

      _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
      _context.SaveChanges();

      return Ok(category);
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
      Category category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
      if(category is null) return NotFound($"Categoria com id={id} não encontrada");

      _context.Categories.Remove(category);
      _context.SaveChanges();

      return Ok(category);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}