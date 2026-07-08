using System.Globalization;

namespace OwlCafe
{
    public static class CurrencyExtensions
    {
        private static readonly CultureInfo IndianCulture = CultureInfo.GetCultureInfo("en-IN");

        public static string ToRupees(this decimal amount)
        {
            return amount.ToString("C", IndianCulture);
        }
    }
}
