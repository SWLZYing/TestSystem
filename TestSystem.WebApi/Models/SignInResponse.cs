using TestSystem.Persistent.Model;

namespace TestSystem.WebApi.Models
{
    public class SignInResponse : BasicResponse
    {
        public Account Acc { get; set; }
    }
}