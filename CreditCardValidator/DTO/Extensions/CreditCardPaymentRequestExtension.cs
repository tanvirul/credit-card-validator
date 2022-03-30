using CreditCardValidator.CreditCards;
using CreditCardValidator.DTO.Request;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;

namespace CreditCardValidator.DTO.Extensions
{
    public static class CreditCardPaymentRequestExtension
    {
        public static CreditCard GetCreditCard(this CreditCardPaymentRequest request)
        {

            return new CreditCard(
                request.CardOwner,
                request.CardNumber,
                request.CVC,
                request.IssueDate.toDate(),
                request.ExpiryDate.toDate()
                );
        }
        public static void AddFluentValidationErrors(this ModelStateDictionary modelState, ValidationResult result, bool usePropertyNames = false)
        {
            if (result.IsValid)
                return;

            foreach (var error in result.Errors)
            {
                // If we exclude the property name the error will show up in the validation summary
                var propertyName = usePropertyNames ? error.PropertyName : "";
                modelState.AddModelError(propertyName, error.ErrorMessage);
            }
        }

        public static DateTime toDate(this string dateString)
        {
            try
            {
                CultureInfo culture = new CultureInfo("en-US");

                if (Common.twoDigitRegex.IsMatch(dateString))
                {
                    culture.Calendar.TwoDigitYearMax = 2099;

                    return DateTime.ParseExact(dateString, "MM/yy", culture);
                }

                return DateTime.ParseExact(dateString, "MM/yyyy", culture);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
