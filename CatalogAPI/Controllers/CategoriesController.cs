using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Models;
using CatalogAPI.Repository;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;

  public CategoriesController(IUnitOfWork context)
  {
    _unitOfWork = context;
  }
  

  [HttpGet]
  public ActionResult<IEnumerable<Category>> Index(){
    try
    {
      List<Category> categories = _unitOfWork.CategoryRepository.Get().ToList();
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
      return _unitOfWork.CategoryRepository.GetCategoryProducts().ToList();
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
      Category? categorie = _unitOfWork?.CategoryRepository.GetById(c => c.CategoryId == id);
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
      _unitOfWork?.CategoryRepository?.Add(category);
      _unitOfWork?.Commit();
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

      _unitOfWork.CategoryRepository.Update(category);

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
      Category category = _unitOfWork.CategoryRepository.GetById(c => c.CategoryId == id);
      if(category is null) return NotFound($"Categoria com id={id} não encontrada");

      _unitOfWork.CategoryRepository.Delete(category);
      _unitOfWork.Commit();

      return Ok(category);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}