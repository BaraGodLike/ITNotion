namespace ITNotion.Notes;

public class DirectoryNotes(string name, DirectoryNotes? parent) : AbstractSource(name, parent)
{

    public SortedSet<AbstractSource>? Backup { get; private set; } = null;
    public SortedSet<AbstractSource> Directory { get; } = [];
    
    
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