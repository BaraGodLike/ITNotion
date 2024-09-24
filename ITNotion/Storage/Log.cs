using ITNotion.Authorization;

namespace ITNotion.Storage;

public static class Log
{
    public static void LogAuthorization(AuthorizationUserDto user, string text)
    {
        if (!Directory.Exists("Storage/Logs/"))
        {
            Directory.CreateDirectory("Storage/Logs");
        }
        File.AppendAllTextAsync("Storage/Logs/logs.txt", $"[{DateTime.Now}] {user.User.GetName()} {text}.\n");
    }
    
    
}