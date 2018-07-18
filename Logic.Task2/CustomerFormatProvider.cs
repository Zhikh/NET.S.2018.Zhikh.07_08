using System;
using System.Globalization;
using Logic.Task1;

namespace Logic.Task2
{
    public sealed class CustomerFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                throw new ArgumentNullException($"The {nameof(arg)} can't be null!");
            }

            Customer customer = arg as Customer;
            if (customer == null)
            {
                throw new ArgumentNullException($"The {nameof(customer)} isn't correct!");
            }
            
            switch (format)
            {
                case "K":
                    return customer.Name.ToUpper();
                case "k":
                    return customer.Name.ToLower();
                case "Z":
                    return "(" + customer.ContactPhone + ")";
                default:
                    try
                    {
                        return customer.ToString(format.ToString(), CultureInfo.CurrentCulture);
                    }
                    catch (FormatException ex)
                    {
                        throw new ArgumentException(String.Format($"Invalid {nameof(format)}!"), ex);
                    }
            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return CultureInfo.CurrentCulture.GetFormat(formatType);
        }
    }
}
