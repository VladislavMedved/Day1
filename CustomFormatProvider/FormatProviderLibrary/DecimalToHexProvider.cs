using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatProviderLibrary
{
    public class DecimalToHexProvider: IFormatProvider, ICustomFormatter
    {
        static readonly string[] hexCharacters = {"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F"};
        IFormatProvider parent;
        public DecimalToHexProvider() : this(CultureInfo.CurrentCulture) { }
        public DecimalToHexProvider(IFormatProvider provider)
        {
            parent = provider;
        }
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }
        public string Format(string format, object arg, IFormatProvider provider)
        {
            if (arg == null || format != "Hx")
                return String.Format(parent,"{0:" + format + "}", arg);
            StringBuilder hexString = new StringBuilder();
            Stack<int> stack = new Stack<int>();
            int popped,delta, dec, providerBase = 16;
            try
            {
                dec = (int)arg;
            }
            catch (InvalidCastException ex)
            {
                
                throw new InvalidCastException("DecimalToHexProvider must recieve numerical type",ex);
            }
            
            while (dec != 0)
            {
                stack.Push(dec);
                dec /= providerBase;
            }
            delta = popped = stack.Pop();
            hexString.Append(hexCharacters[delta]);
            while (stack.Count > 0)
            {
                
                delta = stack.Peek() - popped * providerBase;
                hexString.Append(hexCharacters[delta]);
                popped = stack.Pop();
                
            }
            return hexString.ToString();
        }
    }
}
