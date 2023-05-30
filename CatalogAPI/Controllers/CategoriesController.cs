using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Models;
using CatalogAPI.Repository;
using AutoMapper;
using CatalogAPI.DTOs;
using CatalogAPI.Pagination;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CatalogAPI.Controllers;

//a linha abaixo adiciona a camada de segurança (autenticação) à esse controller
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
  public async Task<ActionResult<IEnumerable<CategoryDTO>>> Index([FromQuery] CategoriesParameters categoriesParameters){
    try
    {
      PagedList<Category> categories = await _unitOfWork.CategoryRepository.GetCategories(categoriesParameters);

      var metadata = new {
        categories.TotalCount,
        categories.PageSize,
        categories.CurrentPage,
        categories.TotalPages,
        categories.HasPrevious,
        categories.HasNext,
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

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
  public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetInnerCategoryProduct(){
    try
    {
      var categories = await _unitOfWork.CategoryRepository.GetCategoryProducts();
      return _mapper.Map<List<CategoryDTO>>(categories);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpGet("{id:int}", Name="ReadCategory")]
  public async Task<ActionResult<CategoryDTO>> Read(int id){
    try
    {
      Category? categorie = await _unitOfWork.CategoryRepository.GetById(c => c.CategoryId == id);
      if(categorie is null) return NotFound($"Categoria com id={id} não encontrada");
      return _mapper.Map<CategoryDTO>(categorie);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPost]
  public async Task<ActionResult> Create(CategoryDTO categoryDTO){
    try
    {
      Category category = _mapper.Map<Category>(categoryDTO);
      if(category is null) return await Task.FromResult<ActionResult>(BadRequest());
      _unitOfWork.CategoryRepository?.Add(category);
      await _unitOfWork.Commit();
      CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);
      return await Task.FromResult<ActionResult>(new CreatedAtRouteResult("ReadCategory", new { id = categoryDto.CategoryId }, categoryDto));
    }
    catch (System.Exception)
    {
      return await Task.FromResult<ActionResult>(StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação"));
    }
  }


  [HttpPut("{id:int}")]
  public async Task<ActionResult> Update(int id, CategoryDTO categoryDTO){
    try
    {
      if(id != categoryDTO.CategoryId) return BadRequest();

      Category category = _mapper.Map<Category>(categoryDTO);

      _unitOfWork.CategoryRepository.Update(category);
      await _unitOfWork.Commit();

      CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);

      return Ok(categoryDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpDelete("{id:int}")]
  public async Task<ActionResult> Delete(int id){
    try
    {
      Category category = await _unitOfWork.CategoryRepository.GetById(c => c.CategoryId == id);
      if(category is null) return NotFound($"Categoria com id={id} não encontrada");

      _unitOfWork.CategoryRepository.Delete(category);
      await _unitOfWork.Commit();
      CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);

      return Ok(categoryDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}