using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Models;
using CatalogAPI.Repository;
using AutoMapper;
using CatalogAPI.DTOs;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public CategoriesController(IUnitOfWork context, IMapper mapper)
  {
    _unitOfWork = context;
    _mapper = mapper;
  }
  

  [HttpGet]
  public ActionResult<IEnumerable<CategoryDTO>> Index(){
    try
    {
      List<Category> categories = _unitOfWork.CategoryRepository.Get().ToList();
      if(categories is null) return NotFound("Categorias não encontradas");
      var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);

      return categoriesDTO;
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpGet("products")]
  public ActionResult<IEnumerable<CategoryDTO>> GetInnerCategoryProduct(){
    try
    {
      var categories =_unitOfWork.CategoryRepository.GetCategoryProducts().ToList();
      return _mapper.Map<List<CategoryDTO>>(categories);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpGet("{id:int}", Name="ReadCategory")]
  public ActionResult<CategoryDTO> Read(int id){
    try
    {
      Category? categorie = _unitOfWork?.CategoryRepository.GetById(c => c.CategoryId == id);
      if(categorie is null) return NotFound($"Categoria com id={id} não encontrada");
      return _mapper.Map<CategoryDTO>(categorie);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPost]
  public ActionResult Create(CategoryDTO categoryDTO){
    try
    {
      Category category = _mapper.Map<Category>(categoryDTO);
      if(category is null) return BadRequest();
      _unitOfWork?.CategoryRepository?.Add(category);
      _unitOfWork?.Commit();
      CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);
      return new CreatedAtRouteResult("ReadCategory", new { id = categoryDto.CategoryId }, categoryDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPut("{id:int}")]
  public ActionResult Update(int id, CategoryDTO categoryDTO){
    try
    {
      if(id != categoryDTO.CategoryId) return BadRequest();

      Category category = _mapper.Map<Category>(categoryDTO);

      _unitOfWork.CategoryRepository.Update(category);

      CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);

      return Ok(categoryDto);
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
      CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);

      return Ok(categoryDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}