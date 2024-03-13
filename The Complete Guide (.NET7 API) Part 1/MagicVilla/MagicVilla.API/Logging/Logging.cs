namespace MagicVilla.API.Logging;

public class Logging : ILogging
{
    public void Log(string mensagem, string tipo)
    {
        if (tipo == "error")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {mensagem}");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        else
        {
            if (tipo == "warning")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Warning: {mensagem}");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(mensagem);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
