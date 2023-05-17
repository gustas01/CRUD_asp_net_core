using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Models;

public class Product
{
  public int ProductId { get; set; }

  [Required]
  [StringLength(80, ErrorMessage = "Nome deve ter no máximo 80 caracteres")]
  public string? Name { get; set; }

  [Required]
  [StringLength(300, ErrorMessage = "Descrição deve ter no máximo 300 caracteres")]
  public string? Description { get; set; }

  [Required]
  [Column(TypeName ="decimal(10,2)")]
  public decimal Price { get; set; }

  [Required]
  [StringLength(300, ErrorMessage = "Descrição deve ter no máximo 300 caracteres")]
  public string? ImageURL { get; set; }

  public float storage { get; set; }

  public DateTime RegisterDate { get; set; }  
}