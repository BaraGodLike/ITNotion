using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;
using Registry = ITNotion.Authorization.Registry;

namespace ITNotion.Commands.StartCommands;

public class RegisterCommand : AbstractCommand
{
    public RegisterCommand()
    {
        Description = "регистрация нового пользователя";
        Name = "reg";
    }
    
    public override async Task<bool> Execute(string? parameter = null)
    {
        var user = await new Registry().Authorize();
        if (user == null) return false;
        var taskLog = Log.LogInformation(new UserDto(user), "register");
        var taskGoToMenu = new Menu(user!).AsyncInit();
        while (!Task.WhenAll(taskLog, taskGoToMenu).IsCompleted) {}
        return false;
    }
}