using System;
using System.Collections.Generic;
using System.Text;
using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class Vehicle : Lifeless, IVehicle
    {
        public Vehicle(long id) : base(id, "Vehicle")
        {
        }
    }
}
