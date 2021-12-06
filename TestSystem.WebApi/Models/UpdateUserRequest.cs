namespace TestSystem.WebApi.Models
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string OldPwd { get; set; }
        public string Pwd { get; set; }
        public string Nickname { get; set; }

    }
}