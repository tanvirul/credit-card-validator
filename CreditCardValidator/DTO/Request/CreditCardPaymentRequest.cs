namespace CreditCardValidator.DTO.Request
{
    public record CreditCardPaymentRequest(
                string CardOwner,
                string CardNumber,
                int CVC,
                string IssueDate,
                string ExpiryDate
        );
}
