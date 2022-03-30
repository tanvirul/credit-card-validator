using CreditCardValidator.CreditCards;
using FluentValidation;

namespace CreditCardValidator.Validators
{
    public class AmericanExpressCardCVCValidator : AbstractValidator<CreditCard>
    {
        public AmericanExpressCardCVCValidator()
        {
            RuleFor(card => card.CVC).Must((cvc) => cvc.ToString().Length == 4)
                .WithMessage("CVC number should be 4 digit");
        }
    }
}
