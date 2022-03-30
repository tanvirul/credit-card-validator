using AutoWrapper.Wrappers;
using CreditCardValidator.CreditCards;
using CreditCardValidator.DTO.Extensions;
using CreditCardValidator.DTO.Request;
using CreditCardValidator.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace CreditCardValidator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpGet]
        [ServiceFilter(typeof(CreditCardValidationFilter))]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Get([FromQuery] CreditCardPaymentRequest request)
        {
            ICreditCard creditCard = request.GetCreditCard();
            return new ApiResponse("", Enum.GetName(typeof(CreditCardType), creditCard.CardType));
        }
    }
}
