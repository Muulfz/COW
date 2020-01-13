using System;
using System.Collections.Generic;
using System.Text;
using Onset.Dimension;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal abstract class Lifeless : Entity, ILifeless
    {
        protected Lifeless(long id, string entityName) : base(id, entityName)
        {
        }

        public void Destroy()
        {
            Wrapper.ExecuteLua("COW_Destroy" + EntityName, new { entity = ID });
            CheckValidation();
        }
    }
}
