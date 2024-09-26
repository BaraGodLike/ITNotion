using ITNotion.Authorization;
using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands.StartCommands;

public class LogInCommand : AbstractCommand
{
    public LogInCommand()
    {
        Description = "авторизация пользователя";
        Name = "login";
    }
    
    public override async Task<bool> Execute(string? parameter = null)
    {
        var user = await new LogIn().Authorize();
        if (user == null) return false; 
        var taskLog = Log.LogInformation(new UserDto(user), "log in");
        var taskGoToMenu = new Menu(user).AsyncInit();
        await Task.WhenAll(taskLog, taskGoToMenu);
        return false;
    }
}