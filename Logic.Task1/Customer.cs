﻿using System;
using System.Globalization;
using System.Text;

namespace Logic.Task1
{
    public class Customer : IFormattable
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public decimal Revenue { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider != null)
            {
                var formatter =  formatProvider.GetFormat(this.GetType())
                    as ICustomFormatter;
                if (formatter != null)
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
                        stringBuilder.Append(Revenue.ToString(format[i].ToString(), 
                            CultureInfo.InvariantCulture));
                        break;
                }
            }

            return stringBuilder.ToString();
        }
    }
}