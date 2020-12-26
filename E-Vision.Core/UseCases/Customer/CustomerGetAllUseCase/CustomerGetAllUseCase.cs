using E_Vision.Core.Interfaces.Repository.Customer;
using E_Vision.Core.UseCases.Base;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static E_Vision.SharedKernel.Enum.SharedKernelEnums;

namespace E_Vision.Core.UseCases.Customer.CustomerGetAllUseCase
{
    public class CustomerGetAllUseCase : BaseUseCase, ICustomerGetAllUseCase
    {
        #region Props
        public ICustomerRepository CustomerRepository { get; set; }
        #endregion
        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<CustomerGetAllOutputDto>> _response)
        {
            #region Vars
            List<CustomerGetAllOutputDto> result = default;
            #endregion
            List<Entities.Customer> customers = await CustomerRepository.GetWhereAsync(x => x.IsDeleted == (byte)DeleteStatus.NotDeleted, $"{nameof(Entities.Vehicle)}");
            if (customers?.Any() ?? default)
            {
                result = Mapping(customers);
            }
            _response.HandlePresenter(new ListResultDto<CustomerGetAllOutputDto>(result, result == default ? default(byte) : result.Count));
            return true;
        }

        private List<CustomerGetAllOutputDto> Mapping(List<Entities.Customer> customers)
        {
            List<CustomerGetAllOutputDto> result = default;
            customers.ForEach(cust => {
                result = cust.Vehicle.Select(v => new CustomerGetAllOutputDto { CustomerId = cust.Id, CustomerName = cust.Name, VehicleId = v.Id, VehicleVIN = v.VIN }).ToList();
            });
            return result;
        }
    }
}
