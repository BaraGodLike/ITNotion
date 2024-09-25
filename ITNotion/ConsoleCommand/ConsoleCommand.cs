namespace ITNotion.ConsoleCommand;

public class ConsoleCommand(Escape escape, ICommandHandler commandHandler)
{

    public async Task GetCommandAsync()
    {
        while (true)
        {
            while (escape.IsPressedEscape)
            { 
                 commandHandler.CommandHandler(ReadCommand());
            }
        }
        
    }

    private static string? ReadCommand()
    {
        return Console.ReadLine();
    }
}
