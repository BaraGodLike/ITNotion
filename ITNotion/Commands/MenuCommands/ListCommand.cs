using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands.MenuCommands;

public class ListCommand : AbstractUserCommand
{

    public ListCommand(User.User user) : base(user)
    {
        Description = "вывод файлов и папок доступных пользователю";
        Name = "list";
    }
    
    public override async Task<bool> Execute(string? parameter = null)
    {
        await new ListPage(User).AsyncInit();
        return false;
    }
}