using System.Text;
using NUnit.Framework;
using Logic.Task1;
using System.Threading;
using System.Globalization;

namespace Logic.Task2.Tests
{
    [TestFixture]
    public class CustomerFormatProviderTests
    {
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "A, N, B", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "A", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "B", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "B, G", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "A, N", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "G", ExpectedResult = "1000000")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "K!", ExpectedResult = "JEFFREY RICHTER!")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "K - k", ExpectedResult = "JEFFREY RICHTER - jeffrey richter")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "A Z", ExpectedResult = "Jeffrey Richter (+1 (425) 555 - 0100)")]
        public string ToString_UnformatValues_FormatString(string name, decimal revenue, string number, string format)
        {
            var customer = new Customer
            {
                Name = name,
                ContactPhone = number,
                Revenue = revenue
            };

            return customer.ToString(format, new CustomerFormatProvider());
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

            var customer = new Customer { Revenue = revenue };
            string actual = customer.ToString(format, new CustomerFormatProvider());

            Assert.AreEqual(expected, actual);
        }
    }
}
