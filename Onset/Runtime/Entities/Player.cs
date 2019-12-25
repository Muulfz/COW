using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class Player : IPlayer
    {
        public int ID { get; }

        public string Name { get; set; }

        public long SteamID { get; private set; }

        internal Player(int id)
        {
            ID = id;
        }
    }
}
