namespace ITNotion.Commands;

public abstract class AbstractUserCommand(User.User user) : AbstractCommand
{
    protected User.User User { get; init; } = user;
    
}