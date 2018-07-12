using System;
using System.Globalization;

namespace Logic.Task1
{
    public sealed class AnotherCustomer : IFormattable
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public decimal Revenue { get; set; }

        public override string ToString()
        {
            return this.ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Creates string for customer based on getting format
        /// </summary>
        /// <param name="format"> Format string </param>
        /// <param name="formatProvider"> Object that provides formatting services for the specified type </param>
        /// <returns> Formated string </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "All";
            }

            if (formatProvider != null)
            {
                if (formatProvider.GetFormat(this.GetType()) is ICustomFormatter formatter)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }

            try
            {
                switch (format.ToUpper())
                {
                    case "NM":
                        return Name;
                    case "PH":
                        return ContactPhone;
                    case "NP":
                        return Name + ", " + ContactPhone;
                    default:
                        return Revenue.ToString(format, CultureInfo.CurrentCulture);
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException(String.Format("The format of '{0}' is invalid.", format), ex);
            }
        }
    }
}
