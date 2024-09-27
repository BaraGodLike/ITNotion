using ITNotion.Notes;
using ITNotion.Pages;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Commands.ListPageCommands;

public class CreateCommand : AbstractUserCommand
{
    private readonly ListPage _page;
    public CreateCommand(User.User user, ListPage page) : base(user)
    {
        _page = page;
        Description = "создать новый файл (в конце укажите .txt) или директорию";
        Name = "create";
    }
    public override async Task<bool> Execute(string? parameter = null)
    {
        var createDir = User.AddSource(await SourceHandler.GetFromString(parameter, _page.CurrentDirectory!));
        var log = Log.LogInformation(new UserDto(User), $"created {parameter}");
        await Task.WhenAll(createDir, log);
        return true;
    }
}