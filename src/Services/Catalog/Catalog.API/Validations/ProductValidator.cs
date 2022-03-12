using Catalog.API.Entities;
using FluentValidation;

namespace Catalog.API.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id).NotEmpty();
            });
            
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
