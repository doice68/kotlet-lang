public class Scanner
{
    readonly string source;
    List<Token> tokens = new();
    int start = 0;
    int current = 0;
    int line = 1;
    readonly Dictionary<string, TokenType> keywords = new()
    {
        {"and",    TokenType.AND},
        {"class",  TokenType.CLASS},
        {"else",   TokenType.ELSE},
        {"false",  TokenType.FALSE},
        {"for",    TokenType.FOR},
        {"fun",    TokenType.FUN},
        {"if",     TokenType.IF},
        {"nil",    TokenType.NIL},
        {"or",     TokenType.OR},
        {"print",  TokenType.PRINT},
        {"return", TokenType.RETURN},
        {"super",  TokenType.SUPER},
        {"this",   TokenType.THIS},
        {"true",   TokenType.TRUE},
        {"var",    TokenType.VAR},
        {"while",  TokenType.WHILE},

    };
    
    
    public Scanner(string source)
    {
        this.source = source;
    }
    public List<Token> ScanTokens()
    {
        while (IsAtEnd() == false)
        {
            start = current;
            ScanToken();
        }
        tokens.Add(new Token(TokenType.EOF, "", null, line));
        return tokens;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c) {
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '{': AddToken(TokenType.LEFT_BRACE); break;
            case '}': AddToken(TokenType.RIGHT_BRACE); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '.': AddToken(TokenType.DOT); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '+': AddToken(TokenType.PLUS); break;
            case ';': AddToken(TokenType.SEMICOLON); break;
            case '*': AddToken(TokenType.STAR); break;
            case '!':
                AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                break;
            case '=':
                AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                break;
            case '<':
                AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                break;
            case '>':
                AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
            case '/':
                if (Match('/')) 
                {
                    while (Peek() != '\n' && IsAtEnd() == false) 
                        Advance();
                } else 
                {
                    AddToken(TokenType.SLASH);
                }
                break;
            case ' ': break;
            case '\r': break;
            case '\t': break;
            case '\n':
                line++;
            break;
            case '"': Str(); break;
            case 'o':
                if (Match('r'))
                    AddToken(TokenType.OR);
                break;
            default:
                if (char.IsDigit(c))
                {
                    Number();
                } 
                else if(char.IsLetter(c))
                {
                    Identifier();
                }
                else
                {
                    ErrorHandler.Error(line, "Unexpected character."); 
                }
                break;
        }
    }

    private void Identifier()
    {
        while (char.IsLetterOrDigit(Peek())) 
            Advance();

        var text = source.Substring(start, current - start);
        TokenType type;
        if (keywords.TryGetValue(text, out type) == false)
        {
            type = TokenType.IDENTIFIER;
        }
        AddToken(type);
    }

    private void Number()
    {
        while (char.IsDigit(Peek())) Advance();
        if (Peek() == '.' && char.IsDigit(PeekNext()))
        {
            Advance();
            while (char.IsDigit(Peek())) Advance();
        }
        Console.WriteLine($"{start}, {current}");
        AddToken(TokenType.NUMBER, double.Parse(source.Substring(start, current - start)));
    }
    private char PeekNext() 
    {
        if (current + 1 >= source.Length) return '\0';
        return source[current + 1];
    } 

    private void Str()
    {
        while (Peek() != '"' && IsAtEnd() == false)
        {
            if (Peek() == '\n') line++;
            Advance();
        }
        if (IsAtEnd())
        {
            ErrorHandler.Error(line, "Unterminated string.");
        }
        Advance();
        var value = source.Substring(start + 1, current - 1);
        AddToken(TokenType.STRING, value);
    }

    bool Match(char expected) {
        if (IsAtEnd()) return false;
        if (source[current] != expected) return false;

        current++;
        return true;
    }
    char Peek()
    {
        if (IsAtEnd()) return '\0';
        return source[current];
    }
    char Advance()
    {
        return source[current++];
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    private void AddToken(TokenType type, object value)
    {
        var text = source.Substring(start, current - start);
        tokens.Add(new Token(type, text, value, line));
    }

    private bool IsAtEnd()
    {
        return current >= source.Length;
    }
}