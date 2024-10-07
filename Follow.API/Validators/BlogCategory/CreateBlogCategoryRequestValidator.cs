using FluentValidation;
using Follow.API.DTO.BlogCategory;

namespace Follow.API.Validators
{
    public class CreateBlogCategoryRequestValidator : AbstractValidator<CreateBlogCategoryRequestDTO>
    {
        public CreateBlogCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Description).MinimumLength(5).WithMessage("Description should be at least 5 characters");
        }
    }
}
