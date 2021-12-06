using TestSystem.Persistent.Model;

namespace TestSystem.WebApi.Models
{
    public class UpdateUserResponse : BasicResponse
    {
        public Account Acc { get; set; }
    }
}