using System;
using System.Globalization;

namespace Logic.Task1
{
    public sealed class Customer : IFormattable
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
        /// <returns> String after formatting </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            if (formatProvider != null)
            {
                var formatter = formatProvider.GetFormat(typeof(ICustomFormatter))
                    as ICustomFormatter;
                if (formatter != null)
                {
                    return formatter.Format(format, this, formatProvider);
                }
            }

            switch (format.ToUpper())
            {
                case "NM":
                    return Name;
                case "PH":
                    return ContactPhone;
                case "NP":
                    return Name + ", " + ContactPhone;
                case "NPG":
                    return Name + ", " + ContactPhone + ", " + Revenue.ToString("G", CultureInfo.CurrentCulture);
                case "NPN":
                    return Name + ", " + ContactPhone + ", " + Revenue.ToString("N", CultureInfo.CurrentCulture);
                case "NGP":
                    return Name + ", " + Revenue.ToString("G", CultureInfo.CurrentCulture) + ", " + ContactPhone;
                case "NNP":
                    return Name + ", " + Revenue.ToString("N", CultureInfo.CurrentCulture) + ", " + ContactPhone;
                case "NG":
                    return Name + ", " + Revenue.ToString("G", CultureInfo.CurrentCulture);
                case "NN":
                    return Name + ", " + Revenue.ToString("N", CultureInfo.CurrentCulture);
                case "PG":
                    return ContactPhone + ", " + Revenue.ToString("G", CultureInfo.CurrentCulture);
                case "PN":
                    return ContactPhone + ", " + Revenue.ToString("N", CultureInfo.CurrentCulture);
                default:
                    try
                    {
                        return Revenue.ToString(format, CultureInfo.CurrentCulture);
                    }
                    catch (FormatException ex)
                    {
                        throw new ArgumentException(($"Invalid {nameof(format)}!"), ex);
                    }
            }
        }
    }
}
