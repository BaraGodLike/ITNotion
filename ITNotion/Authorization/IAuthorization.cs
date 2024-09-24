namespace ITNotion.Authorization;

public interface IAuthorization
{
    public void Authorize();
    public string? GetName();
    
}