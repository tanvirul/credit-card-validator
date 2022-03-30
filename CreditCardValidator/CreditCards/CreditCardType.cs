using System.Text.RegularExpressions;

namespace CreditCardValidator.CreditCards
{
    public enum CreditCardType
    {
        VISA,
        MASTER_CARD,
        AMERICAN_EXPRESS,
        UNSUPPORTED
    }

    public static class Common
    {
        public static readonly Regex fourDigitRegex = new Regex(@"^[\d]{2}[\/][\d]{4}$");
        public static readonly Regex twoDigitRegex = new Regex(@"^[\d]{2}[\/][\d]{2}$");

        public static readonly Regex visaRegex = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
        public static readonly Regex masterCardRegex = new Regex(@"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$");
        public static readonly Regex americanExpressRegex = new Regex(@"^3[47][0-9]{13}$");

    }
}
