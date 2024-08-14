using FluentValidation;
using ProductManagement.Commands;
using System.Linq;

namespace ProductManagement.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x =>  x.Name).NotEmpty().WithMessage("Name must not be empty")
                .MaximumLength(225).WithMessage("Name is over max length");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price must not be empty")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Image).NotNull().WithMessage("Image must not be empty");
            RuleFor(x => x.Quantity).GreaterThan(-1).WithMessage("Quantity must not be negative");
            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.Image != null)
                {
                    string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
                    var extension = Path.GetExtension(request.Image.FileName).ToLower();
                    if (!permittedExtensions.Contains(extension))
                    {
                        context.AddFailure("File type must be image");
                    }
                }
            });
        }
    }
}
