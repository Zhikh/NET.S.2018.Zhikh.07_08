using System.Threading;
using System.Globalization;
using NUnit.Framework;
using Logic.Task1;

namespace Logic.Task2.Tests
{
    class AnotherCustomerFormatProviderTests
    {
        // FIX: problem with type of FormatProvider
        [TestCase("Nm", "N", "Ph", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Nm", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Ph", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("Ph", "G", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("Nm", "N", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("NP", ExpectedResult = "Jeffrey Richter, +1 (425) 555 - 0100")]
        [TestCase("G", ExpectedResult = "1000000")]
        [TestCase("K", ExpectedResult = "JEFFREY RICHTER")]
        public string ToString_UnformatValues_FormatString(params string[] formats)
        {
            var customer = new AnotherCustomer
            {
                Name = "Jeffrey Richter",
                Revenue = 1_000_000m,
                ContactPhone = "+1 (425) 555 - 0100"
            };
            
            string actual = "";
            int i = 0;
            for (; i < formats.Length - 1; i++)
            {
                actual += customer.ToString(formats[i], new AnotherFormatProvider()) + ", ";
            }
            actual += customer.ToString(formats[i], new AnotherFormatProvider());

            return actual;
        }

        [TestCase("P", "en-US")]
        [TestCase("C", "en-US")]
        [TestCase("G", "en-US")]
        [TestCase("P", "ru-ru")]
        [TestCase("C", "ru-ru")]
        [TestCase("G", "ru-ru")]
        [TestCase("P", "sv-FI")]
        [TestCase("C", "sv-FI")]
        [TestCase("G", "sv-FI")]
        [TestCase("P", "tr-TR")]
        [TestCase("C", "tr-TR")]
        [TestCase("G", "tr-TR")]
        [TestCase("P", "de-DE")]
        [TestCase("C", "de-DE")]
        [TestCase("G", "de-DE")]
        public void ToString_CultureDependentValue_FormatString(string format, string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            decimal revenue = 10.30M;
            string expected = revenue.ToString(format, Thread.CurrentThread.CurrentCulture);
            var customer = new AnotherCustomer { Revenue = revenue };
            string actual = customer.ToString(format);

            Assert.AreEqual(expected, actual);
        }
    }
}
