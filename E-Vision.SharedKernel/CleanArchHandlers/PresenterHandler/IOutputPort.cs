namespace E_Vision.SharedKernel.CleanArchHandlers
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void HandlePresenter(TUseCaseResponse response);
    }
}
