using System;
using System.Globalization;
using System.Text;

namespace Logic.Task1
{
    public sealed class Customer : IFormattable
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public decimal Revenue { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                ICustomFormatter formatter =
                    formatProvider.GetFormat(this.GetType())
                            as ICustomFormatter;
                if (formatter != null)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < format.Length; i++)
            {
                switch (format[i])
                {
                    case 'A':
                        sb.Append(Name);
                        break;
                    case 'B':
                        sb.Append(ContactPhone);
                        break;
                    default:
                        sb.Append(Revenue.ToString(format[i].ToString(), CultureInfo.InvariantCulture));
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
