namespace WebAPILibrary.Models
{
    public interface IUserModel
    {
        string EmailAddress { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
    }
}