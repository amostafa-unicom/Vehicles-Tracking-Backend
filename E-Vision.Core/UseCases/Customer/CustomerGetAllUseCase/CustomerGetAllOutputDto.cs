using System.Collections.Generic;

namespace E_Vision.Core.UseCases.Customer.CustomerGetAllUseCase
{
    public class CustomerGetAllOutputDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int VehicleId { get; set; }
        public string VehicleVIN { get; set; }
        public byte Status { get; set; }
    }

}
