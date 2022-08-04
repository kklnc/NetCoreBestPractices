using BP.Api.Models;
using FluentValidation;

namespace BP.Api.Validations
{
    public class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Id).GreaterThan(100).WithMessage("Id 100'den büyük olamaz");
        }
    }
}
