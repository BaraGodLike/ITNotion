using ITNotion.Commands;
using ITNotion.User;

namespace ITNotion.Storage;

public static class Log
{

    private static UserDto unregUser = new UserDto(new User.User("Unauthorized", null));
    
    public static async Task LogInformation(UserDto? user, string text)
    {
        if (!Directory.Exists("Storage/Logs/"))
        {
            Directory.CreateDirectory("Storage/Logs");
        }
        
        await File.AppendAllTextAsync("Storage/Logs/logs.log",
            $"[{DateTime.Now}] -INFORMATION- {user?.User?.Name} {text}.\n");
    }
    
    public static async Task LogInformation(UserDto user, AbstractCommand command)
    {
        if (!Directory.Exists("Storage/Logs/"))
        {
            Directory.CreateDirectory("Storage/Logs");
        }
        
        await File.AppendAllTextAsync("Storage/Logs/logs.log",
            $"[{DateTime.Now}] -INFORMATION- {user.User?.Name} used {command.Name}.\n");
    }
    
    public static async Task LogWarning(Exception exception, UserDto? user = null)
    {
        if (!Directory.Exists("Storage/Logs/"))
        {
            Directory.CreateDirectory("Storage/Logs");
        }
        
        await File.AppendAllTextAsync("Storage/Logs/logs.log",
            $"[{DateTime.Now}] -WARNING- {user?.User?.Name} {exception}.\n");
    }
}
