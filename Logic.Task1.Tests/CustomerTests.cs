using System.Text;
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
            sb.AppendFormat(new CustomFormatProvider(), format, customer);

            return sb.ToString();
        }
    }
}
