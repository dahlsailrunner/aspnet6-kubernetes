using CarvedRock.Shared.Models;

namespace CarvedRock.Api.DomainLogic;

public interface IProductLogic
{
    Task<List<ProductModel>> GetAllProducts();
    Task<ProductModel?> GetProductById(int id);
    Task AddNewProduct(ProductModel productToAdd);
    Task RemoveProduct(int id);
    Task UpdateProduct(ProductModel productToUpdate);
}
