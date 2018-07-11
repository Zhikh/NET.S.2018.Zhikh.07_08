using System;
using System.Threading;

namespace Logic.Task1
{
    public sealed class CustomFormatProvider : IFormatProvider, ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                throw new ArgumentException();
            }

            string result;

            IFormattable formattable = arg as IFormattable;
            if (formattable == null)
            {
                return result = arg.ToString();
            }

            return formattable.ToString(format, formatProvider);
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
        }
    }
}
