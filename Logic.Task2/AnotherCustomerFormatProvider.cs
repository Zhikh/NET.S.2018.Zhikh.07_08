using System;
using System.Globalization;
using Logic.Task1;

namespace Logic.Task2
{
    public sealed class AnotherFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                throw new ArgumentException("Argument can't be null!");
            }

            AnotherCustomer customer = arg as AnotherCustomer;
            if (customer == null)
            {
                throw new ArgumentException("Argument isn't of correct type for this format provider!");
            }
            
            try
            {
                switch (format)
                {
                    case "K":
                        return customer.Name.ToUpper();
                    case "k":
                       return customer.Name.ToLower();
                    case "Z":
                        return "(" + customer.ContactPhone + ")";
                    default:
                        return customer.ToString(format.ToString(), formatProvider);
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException(String.Format("The format of '{0}' is invalid.", format), ex);
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
