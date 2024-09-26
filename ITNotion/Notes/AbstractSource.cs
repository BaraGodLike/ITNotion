namespace ITNotion.Notes;

public abstract class AbstractSource : IComparer<AbstractSource>
{
    public string Path { get; init; }
    
    protected AbstractSource(string name, string directory)
    {
        Name = name;
        Path = $"{directory}/{name}.txt";
        MakeBackup();
    }

    public readonly string Name;
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