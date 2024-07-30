namespace HeroTest.Services
{
    public class ServiceResult
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }

        public ServiceResult(bool succeed, string message) 
        { 
            Succeed = succeed;
            Message = message;
        }

    }


}
