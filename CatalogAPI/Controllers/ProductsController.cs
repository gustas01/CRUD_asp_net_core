using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Models;
using CatalogAPI.Repository;
using AutoMapper;
using CatalogAPI.DTOs;
using CatalogAPI.Pagination;
using Newtonsoft.Json;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public ProductsController(IUnitOfWork context, IMapper mapper)
  {
    _unitOfWork = context;
    _mapper = mapper;
  }


  [HttpGet("orderByPrice")]
  public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByPrice(){

    var products =await _unitOfWork.ProductRepository.GetProductsByPrice();
    var productsDTO = _mapper.Map<List<ProductDTO>>(products);
    return productsDTO;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProductDTO>>> Index([FromQuery] ProductsParameters productsParameters){
    try
    {
      PagedList<Product> products = await _unitOfWork.ProductRepository.GetProducts(productsParameters);

      var metadata = new {
        products.TotalCount,
        products.PageSize,
        products.CurrentPage,
        products.TotalPages,
        products.HasPrevious,
        products.HasNext,
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      if(products is null) return NotFound("Produtos não encontrados");
      var productsDTO = _mapper.Map<List<ProductDTO>>(products);
      return productsDTO;
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }

  [HttpGet("{id:int}", Name="ReadProduct")]
  public async Task<ActionResult<ProductDTO>> Read(int id){
    try
    {
      var product = await _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
      if(product is null) return NotFound("Produto não encontrado");
      return _mapper.Map<ProductDTO>(product);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }


  [HttpPost]
  public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO){
    try
    {
      Product product = _mapper.Map<Product>(productDTO);
      if(product is null) return BadRequest();
      _unitOfWork.ProductRepository.Add(product);
      await _unitOfWork.Commit();
      ProductDTO productDto = _mapper.Map<ProductDTO>(product);
      return new CreatedAtRouteResult("ReadProduct", new { id = productDto.ProductId }, productDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult<ProductDTO>> Update(int id, ProductDTO productDTO){
    try
    {
      if(id != productDTO.ProductId) return BadRequest();

      Product product = _mapper.Map<Product>(productDTO);

      _unitOfWork.ProductRepository.Update(product);
      await _unitOfWork.Commit();

      ProductDTO productDto = _mapper.Map<ProductDTO>(product);

      return Ok(productDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult<ProductDTO>> Delete(int id){
    try
    {
      Product product = await _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
      if(product is null) return NotFound("Produto não encontrado");

      _unitOfWork.ProductRepository.Delete(product);
      await _unitOfWork.Commit();
      ProductDTO productDto = _mapper.Map<ProductDTO>(product);

      return Ok(productDto);
    }
    catch (System.Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
    }
  }
}