using E_Vision.Core.UseCases.Ping;
using E_Vision.SharedKernel.CleanArchHandlers;
using E_Vision.SharedKernel.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Vision.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : BaseController
    {
        #region PRops
        public IPingUseCase pingUseCase { get; set; }
        #endregion

        #region Apis
        [HttpPost]
        public async Task<ActionResult<ResultDto<bool>>> Ping(PingInputDto request)
        {
            OutputPort<ResultDto<bool>> _presenter = new OutputPort<ResultDto<bool>>();
            await pingUseCase.HandleUseCase(request, _presenter);
            return Ok(_presenter.Result);
        }
        #endregion
    }
}