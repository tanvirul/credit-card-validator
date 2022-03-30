using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CreditCardValidator.CreditCards
{
    public class CreditCard : ICreditCard
    {
        public CreditCard() { }
        public CreditCard(string cardOwner,
            string cardNumber,
            int cvc,
            DateTime issueDate,
            DateTime expiryDate)
        {
            this.CardOwner = cardOwner;
            this.CardNumber = cardNumber.Replace(" ", "");
            this.CVC = cvc;
            this.IssueDate = issueDate;
            this.ExpiryDate = expiryDate;
            this.CardType = GetCardType();

        }
        public string CardOwner { get; private set; }
        public string CardNumber { get; private set; }
        public int CVC { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public CreditCardType CardType { get; private set; }

        private CreditCardType GetCardType()
        {
            if (Common.visaRegex.IsMatch(CardNumber))
            {
                return CreditCardType.VISA;
            }
            else if (Common.masterCardRegex.IsMatch(CardNumber))
            {
                return CreditCardType.MASTER_CARD;
            }
            else if (Common.americanExpressRegex.IsMatch(CardNumber))
            {
                return CreditCardType.AMERICAN_EXPRESS;
            }
            else
                return CreditCardType.UNSUPPORTED;

        }
    }
}
