using static System.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        Functions.ImportBultinFunctions();

        var lexer = new Lexer();
        var tokens = lexer.Lex("input print");
        //lexer.Debug(tokens);

        var parser = new Parser();
        var nodes = parser.Parse(tokens);

        var kotlet = new Kotlet();
        kotlet.Run(nodes);
    }
}

class Kotlet
{
    public static List<TType> stack = new List<TType>();
    public void Run(Node[] nodes)
    {
        foreach (var node in nodes)
        {
            node.Run();
        }
    }
}

