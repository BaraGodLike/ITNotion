namespace ITNotion.Commands;

public interface ICommands
{
    void ExecuteCommand(AbstractCommand command);
    AbstractCommand? CommandHandler();
}