using ITNotion.Authorization;

namespace ITNotion.Storage;

public static class Log
{
    public static async Task LogAuthorization(AuthorizationUserDto user, string text)
    {
        if (!Directory.Exists("Storage/Logs/"))
        {
            Directory.CreateDirectory("Storage/Logs");
        }
        await File.AppendAllTextAsync("Storage/Logs/logs.txt", $"[{DateTime.Now}] {user.User.GetName()} {text}.\n");
    }
    
    
}