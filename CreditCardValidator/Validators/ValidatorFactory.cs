using CreditCardValidator.CreditCards;
using FluentValidation;

namespace CreditCardValidator.Validators
{
    public static class ValidatorFactory
    {
        public static IValidator GetValidator(ICreditCard creditCard)
        {
            if (creditCard.CardType == CreditCardType.VISA)
                return new VisaCardCVCValidator();

            else if (creditCard.CardType == CreditCardType.AMERICAN_EXPRESS)
                return new AmericanExpressCardCVCValidator();

            else if (creditCard.CardType == CreditCardType.MASTER_CARD)
                return new MasterCardCVCValidator();
            else
                return null;
        }

    }
}
