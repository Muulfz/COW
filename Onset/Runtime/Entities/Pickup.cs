using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class Pickup : Lifeless, IPickup
    {
        public Vector Scale 
        {
            get => Wrapper.ExecuteLua("COW_Get" + EntityName + "Scale", new { entity = ID }).ExtractPosition();
            set => Wrapper.ExecuteLua("COW_Set" + EntityName + "Scale", new { entity = ID, x = value.X, y = value.Y, z = value.Z });
        }

        public Pickup(long id) : base(id, "Pickup")
        {
        }

        public void SetVisibilityFor(bool visible, params IPlayer[] players)
        {
            foreach (IPlayer player in players)
            {
                player.SetPickupVisibility(this, visible);
            }
        }

        public void SetVisibleFor(params IPlayer[] players)
        {
            SetVisibilityFor(true, players);
        }
    }
}
