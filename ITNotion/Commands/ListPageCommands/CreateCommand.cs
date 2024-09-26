namespace ITNotion.Commands.ListPageCommands;

public class CreateCommand(User.User user) : AbstractUserCommand(user)
{
    public override async Task<bool> Execute(string? parameter = null)
    {
        // user.AddSource()
        return true;
    }
}