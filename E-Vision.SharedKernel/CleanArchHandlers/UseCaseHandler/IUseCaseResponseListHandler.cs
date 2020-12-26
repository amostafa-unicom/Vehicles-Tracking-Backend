using E_Vision.SharedKernel.Dto;
using System.Threading.Tasks;

namespace E_Vision.SharedKernel.CleanArchHandlers
{
    public interface IUseCaseResponseListHandler</*out*/ TUseCaseResponse> 
    {
        Task<bool> HandleUseCase(IOutputPort<ListResultDto<TUseCaseResponse>> _response);
    }
}
