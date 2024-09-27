namespace ITNotion.Authorization;

public interface IAuthorization
{
    Task<User.User?> Authorize();
    Task<bool> InputPassword();
    Task InputName();

}