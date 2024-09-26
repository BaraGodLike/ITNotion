using ITNotion.Exceptions;

namespace ITNotion.Commands;

public class CommandReader(User.User? user)
{
    private User.User? User { get; set; } = user;

    public async Task<string> GetCommand()
    {
        while (true)
        {
            var command = await Console.In.ReadLineAsync();
            if (command is { Length: >= 3 } && command[..2].Equals("--")) return command[2..];
            try
            {
                throw new UnknownCommandException();
            }
            catch (UnknownCommandException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}