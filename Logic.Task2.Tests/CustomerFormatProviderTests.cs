using NUnit.Framework;
using Logic.Task1;

namespace Logic.Task2.Tests
{
    class CustomerFormatProviderTests
    {
        [TestCase("NNP", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555 - 0100")]
        [TestCase("Nm", ExpectedResult = "Jeffrey Richter")]
        [TestCase("Ph", ExpectedResult = "+1 (425) 555 - 0100")]
        [TestCase("PG", ExpectedResult = "+1 (425) 555 - 0100, 1000000")]
        [TestCase("NN", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("NP", ExpectedResult = "Jeffrey Richter, +1 (425) 555 - 0100")]
        [TestCase("G", ExpectedResult = "1000000")]
        [TestCase("K", ExpectedResult = "JEFFREY RICHTER")]
        [TestCase("k", ExpectedResult = "jeffrey richter")]
        [TestCase("Z", ExpectedResult = "(+1 (425) 555 - 0100)")]
        public string ToString_UnformatValues_FormatString(string format)
        {
            var customer = new Customer
            {
                Name = "Jeffrey Richter",
                Revenue = 1_000_000m,
                ContactPhone = "+1 (425) 555 - 0100"
            };
            var provider = new CustomerFormatProvider();
            return customer.ToString(format, provider);
        }
    }
}
