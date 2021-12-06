using TestSystem.Persistent.Model;

namespace TestSystem.WebApi.Models
{
    public class QueryUserResponse : BasicResponse
    {
        public Account Acc { get; set; }
    }
}