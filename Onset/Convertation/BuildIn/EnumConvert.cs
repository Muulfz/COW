using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Convertation.BuildIn
{
    internal class EnumConvert : IConvert
    {
        public bool CanConvert(Type wantedType)
        {
            return wantedType.IsEnum;
        }

        public object Convert(string givenObject, Type wantedType)
        {
            return Enum.Parse(wantedType, givenObject);
        }
    }
}
