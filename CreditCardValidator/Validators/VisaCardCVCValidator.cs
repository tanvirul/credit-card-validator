using CreditCardValidator.CreditCards;
using FluentValidation;

namespace CreditCardValidator.Validators
{
    public class VisaCardCVCValidator : AbstractValidator<CreditCard>
    {
        public VisaCardCVCValidator()
        {
            RuleFor(card => card.CVC).Must((cvc) => cvc.ToString().Length == 3)
                .WithMessage("CVC number should be 3 digit");
        }
    }
}
