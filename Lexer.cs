// enum TokenKind 
// {
//     str,
//     num,
//     varName,

// }
class Token
{
    public string name = "";

    public Token(string name)
    {
        this.name = name;
    }
}
class Lexer
{
    public Token[] Lex(string code)
    {
        code += " ";
        var tokens = new List<Token>();
        var toAdd = "";

        for (int i = 0; i < code.Length; i++)
        {
            // if(i == code.Length - 1)
            if(toAdd != "" && (code[i] == ' ' || code[i] == '\n'))
            {
                tokens.Add(new Token(toAdd));
                toAdd = "";
                continue;
            }
            toAdd += code[i];
        }
        return tokens.ToArray();
    }
    public void Debug(Token[] tokens)
    {
        Console.WriteLine("Lexer Debug::");
        foreach (var token in tokens)
        {
            Console.WriteLine(token.name);
        }
        Console.WriteLine("");
    }
}
