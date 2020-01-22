using System;
using Onset.Entities;
using Onset.Helper;
using Onset.Runtime;

namespace Onset.Converter.BuildIn
{
    internal class PlayerConvert : IConvert
    {
        private static readonly Type PlayerType = typeof(IPlayer);

        public bool CanConvert(Type wantedType)
        {
            return wantedType == PlayerType;
        }

        public object Convert(string givenObject, Type wantedType)
        {
            if (long.TryParse(givenObject, out long val))
            {
                return Wrapper.Server.AllPlayers.SelectFirst(player => player.ID == val,
                    () => Wrapper.Server.AllPlayers.SelectFirst(player => player.SteamID == val));
            }

            return Wrapper.Server.AllPlayers.SelectFirst(player => player.Name == givenObject);
        }
    }
}
