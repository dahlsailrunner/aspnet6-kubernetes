using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Shared.Models;

public class ProductModel 
{
    public int Id { get; set; }

    [DisplayName("Product Name")]
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [DataType(DataType.Currency)]
    [Range(0.00, 10000.00, ErrorMessage = "Value for {0} must be between {1:C} and {2:C}")]
    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Please choose a category.")]
    public int CategoryId { get; set; }

    [DisplayName("Category")]
    public string? CategoryName { get; set; }
}