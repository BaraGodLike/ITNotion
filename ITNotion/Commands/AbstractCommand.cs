namespace ITNotion.Commands;

public abstract class AbstractCommand
{
    public string? Description { get; init; }
    public abstract bool Execute();
}