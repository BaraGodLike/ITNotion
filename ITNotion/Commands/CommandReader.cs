namespace ITNotion.Commands;

public class CommandReader(User.User? user)
{
    private User.User? User { get; set; } = user;

    public async Task<string> GetCommand()
    {
        while (true)
        {
            var command = Console.ReadLine();
            if (command == null || command.Length < 2 || !command[..2].Equals("--"))
            {
                continue;
            }

            return command[2..];
        }
    }
}