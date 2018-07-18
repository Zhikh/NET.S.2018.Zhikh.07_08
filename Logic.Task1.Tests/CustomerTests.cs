using NUnit.Framework;
using System.Globalization;
using System.Threading;

namespace Logic.Task1.Tests
{
    class CustomerTests
    {
        [TestCase("NNP", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Nm", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Ph", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("PG", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("NN", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("NP", ExpectedResult = "Jeffrey Richter, +1 (425) 555 - 0100")]
        [TestCase("G", ExpectedResult = "1000000")]
        public string ToString_UnformatValues_FormatString(string format)
        {
            var customer = new Customer
            {
                Name = "Jeffrey Richter",
                Revenue = 1_000_000m,
                ContactPhone = "+1 (425) 555 - 0100"
            };

            string actual = customer.ToString(format);

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
            decimal revenue = 10.30m;

            string expected = revenue.ToString(format, Thread.CurrentThread.CurrentCulture);

            var customer = new Customer { Revenue = revenue };

            Assert.AreEqual(expected, customer.ToString(format));
        }
    }
}
