using System;

namespace E_Vision.Core.Entities
{
    public class VehicleTracking
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime RequestTime { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
