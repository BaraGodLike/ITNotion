using ITNotion.Storage;
using ITNotion.User;
using Registry = ITNotion.Authorization.Registry;

namespace ITNotion.Commands;

public class RegisterCommand : AbstractCommand
{
    public RegisterCommand()
    {
        Description = "регистрация нового пользователя";
    }
    
    public override bool Execute()
    {
        Log.LogInformation(new UserDto(new Registry().Authorize()), "register");
        return false;
    }
}