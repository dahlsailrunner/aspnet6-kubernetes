using System.ComponentModel.DataAnnotations;

namespace CarvedRock.Api.Data;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public decimal MaxProductPrice { get; set; }
}
