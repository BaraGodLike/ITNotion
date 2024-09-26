namespace ITNotion.Notes;

public abstract class AbstractSource(string name, DirectoryNotes? parent) : IComparer<AbstractSource>
{
    public string Path { get; init; } = $"{parent?.Path}/{parent?.Name}/";
    public DirectoryNotes? Parent { get; init; } = parent;

    public readonly string Name = name;
    public int Compare(AbstractSource? x, AbstractSource? y)
    {
        if (x?.GetType() != y?.GetType())
        {
            return x?.GetType() == typeof(DirectoryNotes) ? 1 : -1;
        }
        return string.CompareOrdinal(x?.Path, y?.Path);
    }
    
    public abstract Task MakeBackup();
}