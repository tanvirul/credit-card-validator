using AutoWrapper.Wrappers;
using CreditCardValidator.CreditCards;
using CreditCardValidator.DTO.Extensions;
using CreditCardValidator.DTO.Request;
using CreditCardValidator.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace CreditCardValidator.Filters
{
    public class CreditCardValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.ActionArguments.TryGetValue("request", out object request);

            CreditCardPaymentRequest paymentRequest = (CreditCardPaymentRequest)request;

            if (!context.ModelState.IsValid)
            {
                throw new ApiProblemDetailsException(context.ModelState);

            }

            ICreditCard creditCard = paymentRequest.GetCreditCard();

            if (creditCard.CardType == CreditCardType.UNSUPPORTED)
            {
                throw new ApiProblemDetailsException("Unsupported Credit card", 422);
            }

            var validator = ValidatorFactory.GetValidator(creditCard);
            var validationContext = new ValidationContext<ICreditCard>(creditCard);
            var result = await validator.ValidateAsync(validationContext);

            if (!result.IsValid)
            {
                context.ModelState.AddFluentValidationErrors(result);
                throw new ApiProblemDetailsException(context.ModelState);
            }

            await next();
        }
    }
}
