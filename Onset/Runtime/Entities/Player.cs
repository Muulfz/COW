using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class Player : IPlayer
    {
        public int ID { get; }

        public string Name
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerName", new {player = ID}).Value<string>("playerName");
            set => Wrapper.ExecuteLua("COW_SetPlayerName", new {player = ID, playerName = value});
        }

        public long SteamID => Wrapper.ExecuteLua("COW_GetPlayerSteamID", new {player = ID}).Value<long>("steamID");

        internal Player(int id)
        {
            ID = id;
        }

        public void CallRemote(string name, params object[] args_)
        {
            Wrapper.ExecuteLua("COW_CallRemoteEvent", new { eventName = name, player = ID, args = args_ });
        }

        public void SendMessage(string message)
        {
            Wrapper.ExecuteLua("COW_AddPlayerChat", new {player = ID, message});
        }
    }
}
