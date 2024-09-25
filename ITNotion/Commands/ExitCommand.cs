namespace ITNotion.Commands;

public class ExitCommand : AbstractCommand
{
    public ExitCommand()
    {
        Description = "закрыть приложение";
    }
    public override bool Execute()
    {
        Environment.Exit(0);
        return false;
    }
}