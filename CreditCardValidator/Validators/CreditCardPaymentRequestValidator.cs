using CreditCardValidator.CreditCards;
using CreditCardValidator.DTO.Extensions;
using CreditCardValidator.DTO.Request;
using FluentValidation;
using System;

namespace CreditCardValidator.Validators
{
    public class CreditCardPaymentRequestValidator : AbstractValidator<CreditCardPaymentRequest>
    {
        public CreditCardPaymentRequestValidator()
        {
            RuleFor(o => o.CardOwner).NotEmpty();

            RuleFor(o => o.CVC).Must(IsValidCVC).WithMessage("Invalid CVC");

            RuleFor(o => o.CardNumber).NotEmpty();

            RuleFor(o => o.CardNumber).Must(IsValidCardNumber)
                .WithMessage("Invalid card number!");

            RuleFor(o => o.IssueDate).Must(IsValidDateFormat).WithMessage("Invalid issue date format.");
            RuleFor(o => o.ExpiryDate).Must(IsValidDateFormat).WithMessage("Invalid expiry date format");

            When(o => IsValidDateFormat(o.IssueDate), () =>
            {
                RuleFor(o => o.IssueDate).Must(IsValidIssueDate).WithMessage("Invalid issue date");
            });

            When(o => IsValidDateFormat(o.ExpiryDate), () =>
            {
                RuleFor(o => o.ExpiryDate).Must(IsValidExpiryDate).WithMessage("Card already expired");
            });
        }


        private bool IsValidExpiryDate(string expiryDate)
        {
            try
            {
                var currentDate = DateTime.Now;

                return DateTime.Compare(expiryDate.toDate(), currentDate) >= 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool IsValidIssueDate(string issueDate)
        {
            try
            {
                var currentDate = DateTime.Now;

                return DateTime.Compare(issueDate.toDate(), currentDate) <= 0; ;

            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool IsValidDateFormat(string dateString)
        {

            if (Common.twoDigitRegex.IsMatch(dateString) ||
                Common.fourDigitRegex.IsMatch(dateString))
                return true;
            else
                return false;
        }

        private bool IsValidCardNumber(string cardNumber)
        {
            try
            {
                return LuhnNet.Luhn.IsValid(cardNumber.Replace(" ", ""));
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool IsValidCVC(int cvc)
        {
            if (cvc.ToString().Length == 3 || cvc.ToString().Length == 4)
                return true;
            else
                return false;
        }
    }
}
