using E_Vision.SharedKernel.Dto;
using System.Threading.Tasks;

namespace E_Vision.SharedKernel.CleanArchHandlers
{
    public interface IUseCaseRequestDynamicResponseHandler<in TUseCaseRequest, dynamic>
    {
        Task<bool> HandleUseCase(TUseCaseRequest _request, IOutputPort<ResultDto<dynamic>> _response);
    }
}
