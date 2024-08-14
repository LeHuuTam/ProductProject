using FluentValidation;
using ProductManagement.Queries;

namespace ProductManagement.Validators
{
    public class FilterByPriceProductValidator : AbstractValidator<FilterByPriceProductQuery>
    {
        public FilterByPriceProductValidator()
        {
            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.MinPrice < 0)
                {
                    context.AddFailure("Min price must be greater than 0");
                }
                if (request.MinPrice > request.MaxPrice)
                {
                    context.AddFailure("Max price must be greater than min price");
                }
            });
        }
    }
}
