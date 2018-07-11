using System;
using System.Text;
using NUnit.Framework;
using Logic.Task1;
using Logic.Task2;


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
    }
}
