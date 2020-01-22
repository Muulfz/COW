using System;

namespace Onset.Converter.BuildIn
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
