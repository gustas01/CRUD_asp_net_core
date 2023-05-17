using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Models;

public class Category {
  public int CategoryId {get; set;}

  [Required]
  [StringLength(80, ErrorMessage = "Nome deve ter no máximo 80 caracteres")]
  public string? Name {get; set;}

  [Required]
  [StringLength(300, ErrorMessage = "URL da imagem deve ter no máximo 300 caracteres")]
  public string? ImageURL {get; set;}

  public ICollection<Product>? Products { get; set; }
}