using System.Collections.Generic;

namespace E_Vision.Core.Entities
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
