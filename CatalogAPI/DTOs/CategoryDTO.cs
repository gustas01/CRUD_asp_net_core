namespace CatalogAPI.DTOs;

public class CategoryDTO{
   public int CategoryId {get; set;}
  public string? Name {get; set;}
  public string? ImageURL {get; set;}

  public ICollection<ProductDTO>? Products { get; set; }
}