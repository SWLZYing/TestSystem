using TestSystem.Persistent.Model;

namespace TestSystem.WebApi.Models
{
    public class CreateUserResponse : BasicResponse
    {
        public Account Acc { get; set; }
        public AccountLevel AccLevel { get; set; }
    }
}