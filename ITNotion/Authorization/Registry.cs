using System.Text.RegularExpressions;
using ITNotion.Storage;

namespace ITNotion.Authorization;

public partial class Registry(Escape escape) : IAuthorization
{
    private string? Name { get; set; }
    private string? Password { get; set; }

    public string? GetName()
    {
        return Name;
    }


    public async Task Authorize()
    {
        escape.IsPressedEscape = false;
        Console.WriteLine("Вы перешли в обычный режим");
        InputName();
        InputPassword();
        await Storage.Storage.SaveRegistryUser(new AuthorizationUserDto(this));
        if (!escape.IsPressedEscape) Console.WriteLine("Вы успешно зарегестрировались!");
        await Log.LogAuthorization(new AuthorizationUserDto(this), "register");
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

            if (Storage.Storage.HasNicknameInStorage(name))
            {
                Console.WriteLine("Данное имя пользователя уже занято.");
                continue;
            }

            Name = name;
            break;
        }
    }

    private async Task InputPassword()
    {
        while (!escape.IsPressedEscape)
        {
            Console.Write("Введите пароль длиной 3-20 символов (a-Z, 0-9, _): ");
            var password = Console.ReadLine();
            if (password == null || password.Length is < 3 or > 20)
            {
                Console.WriteLine("Пароль должен содержать от 3 до 20 символов.");
                continue;
            }

            if (!MyRegex().IsMatch(password))
            {
                Console.WriteLine("Пароль может содержать только латинские буквы, цифры и _");
                continue;
            }

            Console.Write("Повторите пароль: ");
            var passwordRepeat = Console.ReadLine();
            while (!password.Equals(passwordRepeat) && !escape.IsPressedEscape)
            {
                Console.WriteLine("Пароли не совпадают.");
                Console.Write("Повторите пароль: ");
                passwordRepeat = Console.ReadLine();
            }

            Password = Storage.Storage.HashPassword(password);
            break;
        }
    }

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    private static partial Regex MyRegex();
}