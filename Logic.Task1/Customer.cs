using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Logic.Task1
{
    public class Customer : IFormattable
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public decimal Revenue { get; set; }

        /// <summary>
        /// Creates string for customer based on getting format
        /// </summary>
        /// <param name="format"> Format string </param>
        /// <param name="formatProvider"> Object that provides formatting services for the specified type </param>
        /// <returns> Formated string </returns>
        public virtual string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                if (formatProvider.GetFormat(this.GetType()) is ICustomFormatter formatter)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }

            int n = format.Length;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                
                switch (format[i])
                {
                    case 'A':
                        stringBuilder.Append(Name);
                        break;
                    case 'B':
                        stringBuilder.Append(ContactPhone);
                        break;
                    default:
                        if (char.IsPunctuation(format[i]))
                        {
                            stringBuilder.Append(format[i]);
                        }
                        else
                        {
                            stringBuilder.Append(Revenue.ToString(format[i].ToString(),
                                Thread.CurrentThread.CurrentCulture));
                        }
                        break;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
