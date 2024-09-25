namespace ITNotion;

public class Escape
{
    public bool IsPressedEscape { get; set; }
    
    public async Task CheckPressEscape()
    {
        while (true)
        {
            if (Console.ReadKey(true).Key != ConsoleKey.Escape) continue;
            IsPressedEscape = !IsPressedEscape;
            Console.WriteLine($"Вы перешли в {(IsPressedEscape ? "режим ввода комманд" : "обычный режим")}");
            await Task.Delay(500);
        }
    }
}