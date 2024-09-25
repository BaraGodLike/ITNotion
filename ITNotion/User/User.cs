using ITNotion.Commands;

namespace ITNotion.User;

public class User(string name, string password)
{
    public string Name { get; } = name;
    public string? Password { get; } = password;
    public CommandHistory CommandHistory { get; } = new CommandHistory();
}