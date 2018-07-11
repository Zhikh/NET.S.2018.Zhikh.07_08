using Logic.Task1;
using System;
using System.Text;

namespace Logic.Task2
{
    public sealed class CustomerExtension: Customer, IFormattable
    {
        public CustomerExtension(string name, string number, decimal revenue)
        {
            base.ContactPhone = number;
            base.Name = name;
            base.Revenue = revenue;
        }

        public new string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                var formatter = formatProvider.GetFormat(this.GetType())
                    as ICustomFormatter;
                if (formatter != null)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < format.Length; i++)
            {
                switch (format[i])
                {
                    case 'K':
                        stringBuilder.Append(base.Name.ToUpper());
                        break;
                    default:
                        stringBuilder.Append(base.ToString(format[i].ToString(), formatProvider));
                        break;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
