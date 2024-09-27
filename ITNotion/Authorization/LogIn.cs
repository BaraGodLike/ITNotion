using ITNotion.Storage;
using ITNotion.User;

namespace ITNotion.Authorization;

public class LogIn : IAuthorization
{
    private string? Name { get; set; }
    private string? Password { get; set; }
    private User.User? _user;
    private UserDto? _userDto;
    private readonly Storage.Storage _storage = new(new LocalRepository());
    
    public async Task<User.User?> Authorize()
    {
        await InputName();
        if (!await InputPassword()) return _user;
        
        _user = new User.User(Name!, Password!);
        _userDto = new UserDto(_user);
        
        await Console.Out.WriteLineAsync($"Добро пожаловать, {Name}");
        return _user;
    }

    public async Task InputName()
    {
        while (true)
        {
            await Console.Out.WriteAsync("Введите имя пользователя: ");
            var name = await Console.In.ReadLineAsync();
            if (name == null)
            {
                await Console.Out.WriteLineAsync("Имя пользователя не может быть пустым.");
                continue;
            }
            
            if (!_storage.HasNicknameInStorage(name))
            {
                await Console.Out.WriteLineAsync("Пользователь не найден.");
                continue;
            }

            Name = name;
            break;
        }
    }

    public async Task<bool> InputPassword()
    {
        await Console.Out.WriteAsync("Введите пароль: ");
        var password = await Console.In.ReadLineAsync();
        var attempt = 3;
        while (!IsCorrectPassword(password!))
        {
            await Console.Out.WriteLineAsync($"Неверный пароль. Осталось попыток: {attempt}");
            var logTask = Log.LogInformation(_userDto, "failed login attempt");
            var task = Console.Out.WriteAsync("Введите пароль: ");
            await Task.WhenAll(logTask, task);
            password = await Console.In.ReadLineAsync();
            if (attempt-- <= 0) break;
        }

        if (attempt <= 0)
        {
            var writeTask = Console.Out.WriteLineAsync("Слишком много попыток!");
            var logTask = Log.LogInformation(_userDto!, "too many login attempts");
            
            await Task.WhenAll(logTask, writeTask);
            return false;
        }
        Password = password!;
        return true;
    }

    private bool IsCorrectPassword(string password)
    {
        return Storage.Storage.HashPassword(password).
            Equals(_storage.GetUserFromStorage(Name!).Result?.User!.Password);
    }
}