using System;

namespace CreditCardValidator.CreditCards
{
    public interface ICreditCard
    {
        public string CardOwner { get;}
        public string CardNumber { get;}
        public int CVC { get; }
        public DateTime IssueDate { get;}
        public DateTime ExpiryDate { get;}
        public CreditCardType CardType { get; }
    }
}
