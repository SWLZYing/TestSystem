using TestSystem.Persistent.Model;

namespace TestSystem.WebApi.Models
{
    public class SignOutResponse : BasicResponse
    {
        // 0: 失敗 1: 成功
        public bool IsSuccess { get; set; }
    }
}