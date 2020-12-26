using E_Vision.SharedKernel.CleanArchHandlers;

namespace E_Vision.Core.UseCases.Ping
{
    public interface IPingUseCase : IUseCaseRequestResponseHandler<PingInputDto,bool>
    {
    }
}
