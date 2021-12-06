namespace TestSystem.WebApi.Models
{
    public class ResetPwdRequest
    {
        public int Id { get; set; }
        public string OldPwd { get; set; }
        public string Pwd { get; set; }
    }
}