using E_Vision.SharedKernel.Dto;
using System.Threading.Tasks;

namespace E_Vision.SharedKernel.CleanArchHandlers.UseCaseHandler
{
    public interface IUseCaseRequestResponseListHandler<in TUseCaseRequest, /*out*/ TUseCaseResponse>
    //where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse> that where commented because we used generics and the request type can be used with many response types (Ex: IdDto)
    {
        Task<bool> HandleUseCase(TUseCaseRequest request, IOutputPort<ListResultDto<TUseCaseResponse>> outputPort);
    }


}
