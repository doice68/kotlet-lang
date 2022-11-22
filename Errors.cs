public static class Errors
{
    public static void Error(string msg)
    {
        Console.WriteLine("ERROR: " + msg);
        Environment.Exit(-1);
    }
}