namespace ITNotion.Authorization;

public interface IAuthorization
{
    public Task<User.User?> Authorize();
    
}