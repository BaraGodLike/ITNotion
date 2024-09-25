using ITNotion.User;

namespace ITNotion.Storage;

public static class Log
{
    public static void LogInformation(UserDto user, string text)
    {
        if (!Directory.Exists("Storage/Logs/"))
        {
            Directory.CreateDirectory("Storage/Logs");
        }
        
        File.AppendAllTextAsync("Storage/Logs/logs.log",
            $"[{DateTime.Now}] -INFORMATION- {user.User?.Name} {text}.\n");
    }
    
    
}