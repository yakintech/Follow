using FluentValidation;
using Follow.API.DTO.BlogCategory;
using Follow.API.DTO.WebUser;

namespace Follow.API.Validators
{
    public class CreateWebUserRequestValidator : AbstractValidator<CreateWebUserRequestDTO>
    {
        public CreateWebUserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");

        }
    }
}
