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

        /// <summary>
        /// Creates string for customer based on getting format
        /// </summary>
        /// <param name="format"> Format string </param>
        /// <param name="formatProvider"> Object that provides formatting services for the specified type </param>
        /// <returns> Formated string </returns>
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                if (formatProvider.GetFormat(this.GetType()) is ICustomFormatter formatter)
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
                    case 'k':
                        stringBuilder.Append(base.Name.ToLower());
                        break;
                    case 'Z':
                        stringBuilder.Append("(" + base.ContactPhone + ")");
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
