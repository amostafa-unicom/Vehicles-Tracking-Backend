using E_Vision.SharedKernel.Dto;
using System.Threading.Tasks;

namespace E_Vision.SharedKernel.CleanArchHandlers
{
    public interface IUseCaseRequestResponseHandler<in TUseCaseRequest, /*out*/ TUseCaseResponse>
    //where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse> that where commented because we used generics and the request type can be used with many response types (Ex: IdDto)
    {
        Task<bool> HandleUseCase(TUseCaseRequest _request, IOutputPort<ResultDto<TUseCaseResponse>> _response);
    }
}
