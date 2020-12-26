using E_Vision.SharedKernel.Dto;

namespace E_Vision.SharedKernel.CleanArchHandlers
{
    public sealed class OutputPort<T> : IOutputPort<T> where T : ResultBaseDto
    {
        public T Result { get; set; }
        public void HandlePresenter(T response)
        {
            Result = response;
        }
    }
}
