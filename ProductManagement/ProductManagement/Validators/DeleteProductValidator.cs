using FluentValidation;
using ProductManagement.Commands;

namespace ProductManagement.Validators
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id must not be empty");
        }
    }
}
