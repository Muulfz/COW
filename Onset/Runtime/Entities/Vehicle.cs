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
