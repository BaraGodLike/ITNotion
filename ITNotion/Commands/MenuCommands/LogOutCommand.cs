using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands.MenuCommands;

public class LogOutCommand : AbstractUserCommand
{
    public LogOutCommand(User.User user) : base(user)
    {
        Description = "выйти из аккаунта";
        Name = "logout";
    }
    
    public override async Task<bool> Execute(string? parameter = null)
    {
        var taskLog = Log.LogInformation(new UserDto(User), "вышел из аккаунта.");
        var write = Console.Out.WriteLineAsync("Вы вышли из аккаунта.");
        var goToStart = new Start().AsyncInit();

        await Task.WhenAll(taskLog, write, goToStart);
        
        return false;
    }
}