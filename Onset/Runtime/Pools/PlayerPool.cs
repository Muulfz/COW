using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;
using Onset.Runtime.Entities;

namespace Onset.Runtime.Pools
{
    internal class PlayerPool
    {
        internal List<IPlayer> Players { get; }

        internal PlayerPool()
        {
            Players = new List<IPlayer>();
        }

        internal IPlayer GetPlayer(int id)
        {
            lock (Players)
            {
                IPlayer player = SearchPlayer(id);
                if (player != null) return player;
                player = new Player(id);
                Players.Add(player);
                return player;
            }
        }

        internal void RemovePlayer(int id)
        {
            lock (Players)
            {
                IPlayer player = SearchPlayer(id);
                if (player != null)
                {
                    Players.Remove(player);
                }
            }
        }

        private IPlayer SearchPlayer(int id)
        {
            for (int i = Players.Count - 1; i >= 0; i--)
            {
                IPlayer player = Players[i];
                if (player.ID == id)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
