using CarvedRock.Api.Repository;
using CarvedRock.Shared.Models;
using FluentValidation;

namespace CarvedRock.Api.DomainLogic;

public class ProductValidator : AbstractValidator<ProductModel>
{
    public ProductValidator(ICarvedRockRepository repo)
    {
        RuleFor(p => p).MustAsync(async (productModel, cancellation) =>
        {
            if (productModel.CategoryId == 0) return true;
            var cat = await repo.GetCategoryByIdAsync(productModel.CategoryId);
            if (cat?.Name != "Footwear") return true;

            return productModel.Price <= cat.MaxProductPrice || cat.MaxProductPrice == 0;
        }).WithMessage("Price cannot be more than 200.00 for footwear.");
    }
}
