using ITNotion.Storage;

namespace ITNotion.Authorization;

public class LogIn(Escape escape) : IAuthorization
{
    private string? Name { get; set; }
    private string? Password { get; set; }
    public async Task Authorize()
    {
        escape.IsPressedEscape = false;
        Console.WriteLine("Вы перешли в обычный режим");
        InputName();
        while (!InputPassword().IsCompleted)
        {
            if (escape.IsPressedEscape) break;
        }
        if (Password != null)
        {
            Console.WriteLine($"Добро пожаловать, {Name}");
            await Log.LogAuthorization(new AuthorizationUserDto(this), "log in");
        }
    }

    public string? GetName()
    {
        return Name;
    }
    
    private async Task InputName()
    {
        while (!escape.IsPressedEscape)
        {
            Console.Write("Введите имя пользователя: ");
            var name = Console.ReadLine();
            if (name == null)
            {
                Console.WriteLine("Имя пользователя не может быть пустым.");
                continue;
            }
            
            if (!Storage.Storage.HasNicknameInStorage(name))
            {
                Console.WriteLine("Польозователь не найден.");
                continue;
            }

            Name = name;
            break;
        }
    }

    private async Task InputPassword()
    {
        Console.Write("Введите пароль: ");
        var password = Console.ReadLine();
        var attempt = 3;
        while (!IsCorrectPassword(password!) && !escape.IsPressedEscape)
        {
            Console.WriteLine($"Неверный пароль. Осталось попыток: {attempt}");
            await Log.LogAuthorization(new AuthorizationUserDto(this), "failed login attempt");
            Console.Write("Введите пароль: ");
            password = Console.ReadLine();
            if (attempt-- <= 0) break;
        }

        if (attempt <= 0)
        {
            Console.WriteLine("Слишком много попыток!");
            await Log.LogAuthorization(new AuthorizationUserDto(this), "too many login attempts");
            return;
        }
        Password = password!;
    }

    private bool IsCorrectPassword(string password)
    {
        return Storage.Storage.HashPassword(password).Equals(Storage.Storage.UserFromJson(Name!).Result?["User"]["Password"]);
    }
}