using System;
using System.Collections.Generic;
using System.Text;
using Converter = System.Convert;

namespace Onset.Convertation.BuildIn
{
    internal class BasicConvert : IConvert
    {
        internal static readonly Type IntType = typeof(int);
        internal static readonly Type LongType = typeof(long);
        internal static readonly Type ShortType = typeof(short);
        internal static readonly Type ByteType = typeof(byte);
        internal static readonly Type CharType = typeof(char);
        internal static readonly Type FloatType = typeof(float);
        internal static readonly Type DoubleType = typeof(double);
        internal static readonly Type DecimalType = typeof(decimal);
        internal static readonly Type BoolType = typeof(bool);

        public bool CanConvert(Type wantedType)
        {
            return true;
        }

        public object Convert(string givenObject, Type wantedType)
        {
            if (wantedType == IntType)
            {
                return Converter.ToInt32(givenObject);
            }
            if (wantedType == LongType)
            {
                return Converter.ToInt64(givenObject);
            }
            if (wantedType == ShortType)
            {
                return Converter.ToInt16(givenObject);
            }
            if (wantedType == ByteType)
            {
                return Converter.ToByte(givenObject);
            }
            if (wantedType == CharType)
            {
                return Converter.ToChar(givenObject);
            }
            if (wantedType == FloatType)
            {
                return Converter.ToSingle(givenObject);
            }
            if (wantedType == DoubleType)
            {
                return Converter.ToDouble(givenObject);
            }
            if (wantedType == DecimalType)
            {
                return Converter.ToDecimal(givenObject);
            }
            if (wantedType == BoolType)
            {
                return Converter.ToBoolean(givenObject);
            }

            return wantedType;
        }
    }
}
