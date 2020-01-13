using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;
using Onset.Runtime.Entities;

namespace Onset.Runtime.Garbage
{
    internal class EntityHandler : IGarbageHandler<Entity>
    {
        public void Handle(Entity entity)
        {
            if (entity is IPlayer player)
            {
                Wrapper.Server.PlayerPool.RemoveEntity(player);
            }
        }
    }
}
