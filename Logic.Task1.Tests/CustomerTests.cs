using System.Globalization;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Logic.Task1.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A, N, B}", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A}", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:B}", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:B, G}", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A, N}", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:G}", ExpectedResult = "1000000")]
        public string ToString_UnformatValues_FormatString(string name, decimal revenue, string number, string format)
        {
            var customer = new Customer
            {
                Name = name,
                Revenue = revenue,
                ContactPhone = number
            };

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(null, format, customer);

            return sb.ToString();
            // return customer.ToString(format);
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
            decimal revenue = 10.30M;

            string expected = revenue.ToString(format, Thread.CurrentThread.CurrentCulture);

            var customer = new Customer { Revenue = revenue };

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(null, format, customer);

            string actual = sb.ToString();
        }
    }
}
