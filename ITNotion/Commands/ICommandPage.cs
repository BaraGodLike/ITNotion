namespace ITNotion.Commands;

public interface ICommandPage
{
    Task ExecuteCommand(AbstractCommand command);
    Task<AbstractCommand> CommandHandler();
    Task<ICommandPage> AsyncInit();
}