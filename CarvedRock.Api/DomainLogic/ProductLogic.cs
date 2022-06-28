using AutoMapper;
using CarvedRock.Api.Data;
using CarvedRock.Api.Repository;
using CarvedRock.Shared.Models;
using FluentValidation;

namespace CarvedRock.Api.DomainLogic;

public class ProductLogic : IProductLogic
{
    private readonly ICarvedRockRepository _repo;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductModel> _validator;

    public ProductLogic(ICarvedRockRepository repo, IMapper mapper,
        IValidator<ProductModel> validator)
    {
        _repo = repo;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        var products = await _repo.GetAllProductsAsync();
        return _mapper.Map<List<ProductModel>>(products);
    }

    public async Task<ProductModel?> GetProductById(int id)
    {
        var product = await _repo.GetProductByIdAsync(id);
        if (product == null)
            return null;

        var model = _mapper.Map<ProductModel>(product);
        return model;
    }

    public async Task AddNewProduct(ProductModel productToAdd)
    {
        await _validator.ValidateAndThrowAsync(productToAdd);
        var productToSave = _mapper.Map<Product>(productToAdd);
        await _repo.AddProductAsync(productToSave);
    }

    public async Task RemoveProduct(int id)
    {
        await _repo.RemoveProductAsync(id);
    }

    public async Task UpdateProduct(ProductModel productToUpdate)
    {
        await _validator.ValidateAndThrowAsync(productToUpdate);
        var productToSave = _mapper.Map<Product>(productToUpdate);
        await _repo.UpdateProductAsync(productToSave);
    }
}

