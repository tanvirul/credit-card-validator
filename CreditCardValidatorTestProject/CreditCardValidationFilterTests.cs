using AutoWrapper.Wrappers;
using CreditCardValidator.DTO.Request;
using CreditCardValidator.Filters;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CreditCardValidatorTestProject
{
    public class CreditCardValidationFilterTests
    {

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "Visa card payment")]
        public async Task Validate_VisaCard_ShouldSuccessAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "4929308000143955",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 123
            );

            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().NotThrowAsync<ApiProblemDetailsException>();
            await act.Should().NotThrowAsync<Exception>();
        }

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "Visa card payment")]
        public async Task Invalid_VisaCard_ShouldThrowExceptionAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "411111111101000",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 1234
            );

            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().ThrowAsync<ApiProblemDetailsException>();
        }

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "Master card payment")]
        public async Task Validate_MasterCard_ShouldSuccessAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "5555555555554444",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 123
            );

            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().NotThrowAsync<ApiProblemDetailsException>();
        }

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "Master card payment")]
        public async Task Invalid_MasterCard_ShouldThrowExceptionAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "5555555555554444",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 1235
            );
            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().ThrowAsync<ApiProblemDetailsException>();
        }

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "AmericanExpress card payment")]
        public async Task Validate_AmericanExpress_ShouldSuccessAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "378282246310005",
                IssueDate : "11/20",
                ExpiryDate : "02/2025",
                CVC : 1234
            );

            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().NotThrowAsync<ApiProblemDetailsException>();
        }

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "AmericanExpress card payment")]
        public async Task Invalid_AmericanExpress_ShouldThrowExceptionAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "378282246310005",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 123
            );

            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().ThrowAsync<ApiProblemDetailsException>();

        }

        [Trait("PaymentsController", "Payment request validation")]
        [Fact(DisplayName = "Unknown card payment")]
        public async Task Unknown_CreditCard_ShouldThrowExceptionAsync()
        {
            var mCreditCardRequest = new CreditCardPaymentRequest
            (
                CardOwner : "Mr.X",
                CardNumber : "869946514761018",
                IssueDate : "11/20",
                ExpiryDate : "02/25",
                CVC : 123
            );

            var actExecutingContext = GetExecutingContext(mCreditCardRequest);

            var valInputObject = new CreditCardValidationFilter();
            Func<Task> act = async () => await valInputObject.OnActionExecutionAsync(actExecutingContext, Mock.Of<ActionExecutionDelegate>());

            await act.Should().ThrowAsync<ApiProblemDetailsException>();
        }

        private ActionExecutingContext GetExecutingContext(CreditCardPaymentRequest request)
        {
            var actContext = new ActionContext(
               Mock.Of<HttpContext>(),
               Mock.Of<RouteData>(),
               Mock.Of<Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor>(),
               Mock.Of<Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary>()
           );

            var actExecutingContext = new Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext(
                actContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            actExecutingContext.ActionArguments["request"] = request;
            return actExecutingContext;
        }
    }
}
