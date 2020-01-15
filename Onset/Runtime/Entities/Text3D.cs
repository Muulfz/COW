using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Entities;
using Onset.Enums;

namespace Onset.Runtime.Entities
{
    internal class Text3D : Lifeless, IText3D
    {
        public new Vector Position
        {
            get => throw new NotImplementedException("Position Getter is illegal on Text3D!");
            set => throw new NotImplementedException("Position Setter is illegal on Text3D!");
        }

        public string Text
        {
            set => Wrapper.ExecuteLua("COW_SetText3DText", new { entity = ID, text = value });
        }

        public Text3D(long id) : base(id, "Text3D")
        {
        }

        public void SetVisibilityFor(bool visible, params IPlayer[] players)
        {
            foreach (IPlayer player in players)
            {
                player.SetText3DVisibility(this, visible);
            }
        }

        public void SetVisibleFor(params IPlayer[] players)
        {
            SetVisibilityFor(true, players);
        }

        public bool AttachTo(AttachType type, IEntity entityTo, Vector position, Vector r = null, string socketName = "")
        {
            r = r ?? Vector.Empty;
            throw new NotImplementedException(); //TODO Implement attaching
        }
    }
}
