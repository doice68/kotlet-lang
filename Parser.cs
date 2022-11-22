class Parser
{
    public Node[] Parse(Token[] tokens)
    {
        var nodes = new List<Node>();
        for (int i = 0; i < tokens.Length; i++)
        {
            if(tokens[i].name == "print")
                nodes.Add(new PrintNode());
            else if(Functions.functions.ContainsKey(tokens[i].name))
                nodes.Add(new Func(Functions.functions[tokens[i].name]));
            else
                nodes.Add(new StackPush(ConvertToType(tokens[i].name)));
        }
        return nodes.ToArray();
    }
    public static TType ConvertToType(string name)
    {
        float i;
        if(float.TryParse(name, out i))
        {
            return new Num(i);
        }
        // null type
        return new TType();
    }
}