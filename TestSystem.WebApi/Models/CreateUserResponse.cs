using TestSystem.Persistent.Model;

namespace TestSystem.WebApi.Models
{
    public class CreateUserResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Account Data { get; set; }
    }
}