namespace ITNotion.Authorization;

public interface IAuthorization
{
    public User.User? Authorize();
    
}