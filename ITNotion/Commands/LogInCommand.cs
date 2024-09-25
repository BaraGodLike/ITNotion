using ITNotion.Authorization;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands;

public class LogInCommand : AbstractCommand
{
    public LogInCommand()
    {
        Description = "авторзиация пользователя";
    }
    
    public override bool Execute()
    {
        Log.LogInformation(new UserDto(new LogIn().Authorize()), "log in");
        return false;
    }
}