using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Api.Data;

public class AdminContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    private readonly string _adminConnStr;

    public AdminContext(IConfiguration config)
    {
        _adminConnStr = config.GetConnectionString("AdminDatabase");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(_adminConnStr);

    public void SeedInitialData()
    {
        if (Products.Any())
        {
            Products.RemoveRange(Products);
            SaveChanges();
        }

        if (Categories.Any())
        {
            Categories.RemoveRange(Categories);
            SaveChanges();
        }

        var footwearCat = new Category { Id = 1000, Name = "Footwear", MaxProductPrice = 299.00M };
        var equipmentCat = new Category { Id = 2000, Name = "Equipment", MaxProductPrice = 999.99M };

        Products.Add(new Product
        {
            Id = 1, Name = "Trailblazer", Price = 69.99M, IsActive = true, Category = footwearCat,
            Description = "Great support in this high-top to take you to great heights and trails."
        });
        Products.Add(new Product
        {
            Id = 2, Name = "Coastliner", Price = 49.99M, IsActive = true, Category = footwearCat,
            Description =
                "Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats."
        });
        Products.Add(new Product
        {
            Id = 3, Name = "Woodsman", Price = 64.99M, IsActive = true, Category = footwearCat,
            Description =
                "All the insulation and support you need when wandering the rugged trails of the woods and backcountry."
        });
        Products.Add(new Product
        {
            Id = 4, Name = "Basecamp", Price = 249.99M, IsActive = true, Category = equipmentCat,
            Description = "Great insulation and plenty of room for 2 in this spacious but highly-portable tent."
        });
        Categories.Add(footwearCat);
        Categories.Add(equipmentCat);

        SaveChanges();
    }
}

