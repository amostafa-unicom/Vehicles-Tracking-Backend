namespace E_Vision.SharedKernel.Dto
{
    public class ResultBaseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ResultBaseDto(bool isSuccess = true, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
