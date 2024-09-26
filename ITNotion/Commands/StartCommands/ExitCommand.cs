namespace ITNotion.Commands.StartCommands;

public class ExitCommand : AbstractCommand
{
    public ExitCommand()
    {
        Description = "закрыть приложение";
        Name = "exit";
    }
    public override async Task<bool> Execute(string? parameter = null)
    {
        Environment.Exit(0);
        return false;
    }
}