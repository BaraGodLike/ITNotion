using System.Text.RegularExpressions;
using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Authorization;

public partial class Registry : IAuthorization
{
    private string? Name { get; set; }
    private string? Password { get; set; }
    private User.User? _user;
    private readonly Storage.Storage _storage = new(new LocalRepository());
    
    public async Task<User.User?> Authorize()
    {
        await InputName();
        if (!await InputPassword()) return _user;
        
        _user = new User.User(Name!, Password!);
        
        var taskSave = _storage.SaveRegistryUser(new UserDto(_user));
        var taskOutput = Console.Out.WriteLineAsync("Вы успешно зарегестрировались!");

        await Task.WhenAll(taskSave, taskOutput);
        return _user;
    }

    public async Task InputName()
    {
        while (true)
        {
            Console.Write("Введите имя пользователя: ");
            var name = await Console.In.ReadLineAsync();
            if (name == null)
            {
                await Console.Out.WriteLineAsync("Имя пользователя не может быть пустым.");
                continue;
            }

            if (_storage.HasNicknameInStorage(name))
            {
                await Console.Out.WriteLineAsync("Данное имя пользователя уже занято.");
                continue;
            }

            Name = name;
            break;
        }
    }

    public async Task<bool> InputPassword()
    {
        while (true)
        {
            Console.Write("Введите пароль длиной 3-20 символов (a-Z, 0-9, _): ");
            var password = await Console.In.ReadLineAsync();
            if (password == null || password.Length is < 3 or > 20)
            {
                await Console.Out.WriteLineAsync("Пароль должен содержать от 3 до 20 символов.");
                continue;
            }

            if (!MyRegex().IsMatch(password))
            {
                await Console.Out.WriteLineAsync("Пароль может содержать только латинские буквы, цифры и _");
                continue;
            }

            Console.Write("Повторите пароль: ");
            var passwordRepeat = await Console.In.ReadLineAsync();
            while (!password.Equals(passwordRepeat))
            {
                await Console.Out.WriteLineAsync("Пароли не совпадают.");
                Console.Write("Повторите пароль: ");
                passwordRepeat = await Console.In.ReadLineAsync();
            }
            Password = Storage.Storage.HashPassword(password);
            return true;
        }
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    private static partial Regex MyRegex();
}