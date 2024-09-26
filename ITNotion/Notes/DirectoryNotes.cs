namespace ITNotion.Notes;

public class DirectoryNotes(string name, string directory) : AbstractSource(name, directory)
{
    
    public SortedSet<AbstractSource>? Backup { get; private set; }
    public SortedSet<AbstractSource> Directory { get; init; } = [];
    
    public override async Task MakeBackup()
    {
        Backup = [..Directory];
    }

    public override string ToString()
    {
        return $"- {Name}";
    }

    public async Task AddSource(AbstractSource source)
    {
        Directory.Add(source);
    }
}