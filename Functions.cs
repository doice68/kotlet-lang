public static class Functions
{
    public static Dictionary<string, Action> functions = new Dictionary<string, Action>();
    public static void ImportBultinFunctions()
    {
        functions.Add("input", () => 
        {
            Kotlet.stack.Add(new Str(Console.ReadLine()));
        });
    }
}