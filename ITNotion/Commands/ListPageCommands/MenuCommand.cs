using ITNotion.Pages;

namespace ITNotion.Commands.ListPageCommands;

public class MenuCommand(User.User user) : AbstractUserCommand(user)
{
    
    
    public override async Task<bool> Execute(string? parameter = null)
    {
        await new Menu(User).AsyncInit();
        return false;
    }
}