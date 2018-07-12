using NUnit.Framework;
using System.Globalization;
using System.Threading;

namespace Logic.Task1.Tests
{
    class AnotherCustomerTests
    {
        [TestCase("Nm","N","Ph", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Nm", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Ph", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("Ph", "G", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("Nm", "N", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("NP", ExpectedResult = "Jeffrey Richter, +1 (425) 555 - 0100")]
        [TestCase("G", ExpectedResult = "1000000")]
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
                actual += customer.ToString(formats[i]) + ", ";
            }
            actual += customer.ToString(formats[i]);

            return actual;
        }

        [TestCase("{0: P}", "en-US")]
        [TestCase("{0: C}", "en-US")]
        [TestCase("{0: G}", "en-US")]
        [TestCase("{0: P}", "ru-ru")]
        [TestCase("{0: C}", "ru-ru")]
        [TestCase("{0: G}", "ru-ru")]
        [TestCase("{0: P}", "sv-FI")]
        [TestCase("{0: C}", "sv-FI")]
        [TestCase("{0: G}", "sv-FI")]
        [TestCase("{0: P}", "tr-TR")]
        [TestCase("{0: C}", "tr-TR")]
        [TestCase("{0: G}", "tr-TR")]
        [TestCase("{0: P}", "de-DE")]
        [TestCase("{0: C}", "de-DE")]
        [TestCase("{0: G}", "de-DE")]
        public void ToString_CultureDependentValue_FormatString(string format, string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            decimal revenue = 10.30m;

            string expected = revenue.ToString(format, Thread.CurrentThread.CurrentCulture);

            var customer = new AnotherCustomer { Revenue = revenue };

            Assert.AreEqual(expected, customer.ToString(format));
        }
    }
}
