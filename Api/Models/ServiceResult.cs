namespace Api.Models
{
    public class ServiceResult<TModel>
    {
        public int Code { get; set; } = 0;
        public string Message { get; set; } = "Ok.";
        public Status Status { get; set; }
        public TModel Result { get; set; }
        
        public ServiceResult(TModel result, int code, string message, Status status) : this(result, code, message)
        {
            Status = status;
        }
        
        public ServiceResult(TModel result, int code, string message) : this(result, code)
        {
            Message = message;
        }
        
        public ServiceResult(TModel result, int code) : this(result)
        {
            Code = 204;
        }

        public ServiceResult(TModel result)
        {
            if (result == null)
            {
                Code = 204;
                Message = "Kayıt bulunamadı !";
                Status = Status.Info;
            }else
                Result = result;
        }
    }

    public enum Status
    {
        Success,
        Error,
        Warning,
        Info
    }
}