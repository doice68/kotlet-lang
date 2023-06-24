public class Kotlet
{
    public Kotlet(string[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("usage: kotlet [script]");
            Environment.Exit(64);
        }
        else if(args.Length == 1)
        {
            RunFile(args[0]);
        }
        else
        {
            RunPrompt();
        }
    }
    void RunPrompt()
    {
        while (true)
        {
            Console.Write("> ");

            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                break;

            Run(line);
            ErrorHandler.hadError = false;
        }
    }

    void RunFile(string dir)
    {
        if (ErrorHandler.hadError)
            Environment.Exit(65);

        Run(File.ReadAllText(dir));
    }

    void Run(string text)
    {
        Scanner scanner = new(text);
        List<Token> tokens = scanner.ScanTokens();

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}