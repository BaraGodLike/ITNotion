namespace ITNotion.Authorization;

public interface IAuthorization
{
    public Task Authorize();
    public string? GetName();
    
}