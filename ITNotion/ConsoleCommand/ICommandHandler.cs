namespace ITNotion.ConsoleCommand;

public interface ICommandHandler
{
    Task CommandHandler(string? command);
    Task Help();
    Task Exit();
}