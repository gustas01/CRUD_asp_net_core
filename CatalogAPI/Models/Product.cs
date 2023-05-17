namespace CatalogAPI.Models;

public class Product
{
  public int ProductId { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public decimal Price { get; set; }
  public string? ImageURL { get; set; }
  public float storage { get; set; }
  public DateTime RegisterDate { get; set; }  
}