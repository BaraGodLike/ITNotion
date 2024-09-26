namespace ITNotion.Commands;

public abstract class AbstractCommand
{
    public string? Description { get; protected init; }
    public string? Name { get; protected init; }
    public abstract Task<bool> Execute(string? parameter = null);
}