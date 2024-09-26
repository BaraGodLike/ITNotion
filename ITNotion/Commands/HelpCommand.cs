namespace ITNotion.Commands;

public class HelpCommand : AbstractCommand
{
    public HelpCommand(Dictionary<string, AbstractCommand> map)
    {
        _map = map;
        Description = "вывод списка доступных команд";
        Name = "help";
    }
    
    private readonly Dictionary<string, AbstractCommand> _map;

    public override async Task<bool> Execute(string? parameter = null)
    {
        await Console.Out.WriteLineAsync("Список доступных комманд:");
        foreach (var key in _map)
        {
            await Console.Out.WriteLineAsync($"\t--{key.Key}\t{key.Value.Description}");
        }
        
        return false;
    }
}