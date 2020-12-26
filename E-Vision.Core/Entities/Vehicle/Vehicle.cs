using System;
using System.Collections.Generic;

namespace E_Vision.Core.Entities
{
    public class Vehicle:BaseEntity
    {
        public string VIN { get; set; }
        public string Model { get; set; }
        public int CustomerId { get; set; }
        public DateTime LastPingTime { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<VehicleTracking> VehicleTracking { get; set; }
    }
}
