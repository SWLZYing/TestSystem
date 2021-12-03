namespace TestSystem.WebApi.Models
{
    public class CreateUserRequest
    {
        public string Acc { get; set; }
        public string Pwd { get; set; }
        public string Nickname { get; set; }
    }
}