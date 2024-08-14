using FluentValidation;
using ProductManagement.Queries;

namespace ProductManagement.Validators
{
    public class GetByIdProductValidator : AbstractValidator<GetByIdProductQuery>
    {
        public GetByIdProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty");
        }
    }
}
