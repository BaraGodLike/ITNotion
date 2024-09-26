using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands.ListPageCommands;

public class CdCommand(User.User user, ListPage page) : AbstractUserCommand(user)
{
    public override async Task<bool> Execute(string? parameter = null)
    {
        await page.ToDirectory(parameter);
        await page.PrintCurrentDirectory();
        return false;
    }
}