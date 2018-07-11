using System;
using System.Text;
using NUnit.Framework;
using Logic.Task1;
using Logic.Task2;
using System.Threading;
using System.Globalization;

namespace Logic.Task2.Tests
{
    [TestFixture]
    public class CustomerExtensionTests
    {
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A, N, B}", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A}", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:B}", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:B, G}", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A, N}", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:G}", ExpectedResult = "1000000")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:K!}", ExpectedResult = "JEFFREY RICHTER!")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:K - k}", ExpectedResult = "JEFFREY RICHTER - jeffrey richter")]
        [TestCase("Jeffrey Richter", 1_000_000, "+1 (425) 555 - 0100", "{0:A Z}", ExpectedResult = "Jeffrey Richter (+1 (425) 555 - 0100)")]
        public string ToString_UnformatValues_FormatString(string name, decimal revenue, string number, string format)
        {
            var customer = new CustomerExtension(name, number, revenue);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(new CustomFormatProvider(), format, customer);

            return sb.ToString();
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
            sb.AppendFormat(new CustomFormatProvider(), format, customer);

            string actual = sb.ToString();
        }
    }
}
