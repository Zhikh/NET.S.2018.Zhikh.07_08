using System;
using System.Globalization;
using System.Text;

namespace Logic.Task1
{
    public sealed class CustomerFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                throw new ArgumentException("Argument can't be null!");
            }

            Customer customer = arg as Customer;
            if (customer == null)
            {
                throw new ArgumentException("Argument isn't of correct type for this format provider!");
            }

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < format.Length; i++)
            {
                try
                {
                    switch (format[i])
                    {
                        case 'K':
                            stringBuilder.Append(customer.Name.ToUpper());
                            break;
                        case 'k':
                            stringBuilder.Append(customer.Name.ToLower());
                            break;
                        case 'Z':
                            stringBuilder.Append("(" + customer.ContactPhone + ")");
                            break;
                        default:
                            stringBuilder.Append(customer.ToString(format[i].ToString(), formatProvider));
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid.", format[i]), ex);
                }
            }

            return stringBuilder.ToString();
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
