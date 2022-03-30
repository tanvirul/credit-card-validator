using CreditCardValidator.DTO.Request;
using CreditCardValidator.Validators;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CreditCardValidatorTestProject
{
    public class CreditCardPaymentRequestValidatorTests
    {
        [Trait("CreditCardPaymentRequestValidator", "Payment request validation")]
        [Fact(DisplayName = "Credit card payment")]
        public async Task Validate_Request_InvalidCVC()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "4111111111111100",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 12356
            );

            var mValidator = new CreditCardPaymentRequestValidator();

            var result = await mValidator.ValidateAsync(mCreditCardRequest);

            result.IsValid.Should().BeFalse();

            result.Errors.Select(x => x.PropertyName).Should().NotBeEmpty()
                .And
                .ContainMatch("CVC");
        }


        [Trait("CreditCardPaymentRequestValidator", "Payment request validation")]
        [Fact(DisplayName = "Credit card payment")]
        public async Task Validate_Request_InvalidCardNumber()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest(
            
                CardOwner : "Mr.X",
                CardNumber : "4929308000143900",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 123
            );

            var mValidator = new CreditCardPaymentRequestValidator();

            var result = await mValidator.ValidateAsync(mCreditCardRequest);

            result.IsValid.Should().BeFalse();

            result.Errors.Select(x => x.PropertyName).Should().NotBeEmpty()
                .And
                .ContainMatch("CardNumber");
        }

        [Trait("CreditCardPaymentRequestValidator", "Payment request validation")]
        [Fact(DisplayName = "Credit card payment")]
        public async Task Validate_Request_InvalidExpiryDate()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "4929308000143955",
                IssueDate : "11/20",
                ExpiryDate : "02/20",
                CVC : 123
            );

            var mValidator = new CreditCardPaymentRequestValidator();

            var result = await mValidator.ValidateAsync(mCreditCardRequest);

            result.IsValid.Should().BeFalse();

            result.Errors.Select(x => x.PropertyName).Should().NotBeEmpty()
                .And
                .ContainMatch("ExpiryDate");
        }
    }
}
