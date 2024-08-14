using FluentValidation;
using ProductManagement.Queries;

namespace ProductManagement.Validators
{
    public class FilterByQuantityProductValidator : AbstractValidator<FilterByQuantityProductQuery>
    {
        public FilterByQuantityProductValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity must not be empty")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
