using ITNotion.Commands;
using ITNotion.Notes;

namespace ITNotion.User;

public class User(string name, string password)
{
    public string Name { get; } = name;
    public string? Password { get; } = password;
    public CommandHistory CommandHistory { get; } = new CommandHistory();
    public DirectoryNotes Sources { get; init; } = new DirectoryNotes("Main", "/");

    public async Task AddSource(AbstractSource source)
    {
        await Sources.AddSource(source);
    }
}