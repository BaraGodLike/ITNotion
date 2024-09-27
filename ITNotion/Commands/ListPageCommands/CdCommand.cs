using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands.ListPageCommands;

public class CdCommand : AbstractUserCommand
{
    private readonly ListPage _page;
    public CdCommand(User.User user, ListPage page) : base(user)
    {
        _page = page;
        Description = "переход в директорию из списка";
        Name = "cd";
    }
    
    
    public override async Task<bool> Execute(string? parameter = null)
    {
        await _page.ToDirectory(parameter);
        await _page.PrintCurrentDirectory();
        return false;
    }
}