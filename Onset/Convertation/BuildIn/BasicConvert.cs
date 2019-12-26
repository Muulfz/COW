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
                return int.Parse(givenObject);
            }
            if (wantedType == LongType)
            {
                return long.Parse(givenObject);
            }
            if (wantedType == ShortType)
            {
                return short.Parse(givenObject);
            }
            if (wantedType == ByteType)
            {
                return byte.Parse(givenObject);
            }
            if (wantedType == CharType)
            {
                return char.Parse(givenObject);
            }
            if (wantedType == FloatType)
            {
                return float.Parse(givenObject);
            }
            if (wantedType == DoubleType)
            {
                return double.Parse(givenObject);
            }
            if (wantedType == DecimalType)
            {
                return decimal.Parse(givenObject);
            }
            if (wantedType == BoolType)
            {
                return bool.Parse(givenObject);
            }

            return givenObject;
        }
    }
}
