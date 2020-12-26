using E_Vision.Core.UseCases.Customer.CustomerGetAllUseCase;
using E_Vision.Core.UseCases.Tracking.GetAllDisConnectedVehicleUseCase;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Vision.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        #region PRops
        public ICustomerGetAllUseCase CustomerGetAllUseCase { get; set; } 
        #endregion

        #region Apis
        [HttpPost]
        public async Task<ActionResult<ListResultDto<CustomerGetAllOutputDto>>> GetAll()
        {
            OutputPort<ListResultDto<CustomerGetAllOutputDto>> _presenter = new OutputPort<ListResultDto<CustomerGetAllOutputDto>>();
            await CustomerGetAllUseCase.HandleUseCase( _presenter);
            return Ok(_presenter.Result);
        } 
        #endregion
    }
}