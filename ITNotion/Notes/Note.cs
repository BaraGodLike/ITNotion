namespace ITNotion.Notes;

public class Note(string name, DirectoryNotes? parent) : AbstractSource(name, parent)
{
    
    public string[]? Backup { get; private set; }
    
    private static async Task<string[]> ReadAllLinesAsync(string path)
    {
        var lines = new List<string>();
        await using var openStream = File.OpenRead(path);
        using (var reader = new StreamReader(openStream))
        {
            while (await reader.ReadLineAsync() is { } line)
            {
                lines.Add(line);
            }
        }
        return lines.ToArray();
    }

    public override async Task MakeBackup()
    {
        Backup = await ReadAllLinesAsync(Path);
    }

    public override string ToString()
    {
        return $"* {Name}.txt";
    }
}