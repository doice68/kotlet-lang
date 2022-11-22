using static System.Console;
class Node
{
    public virtual void Run(){}
    public TType GetLastStackItem()
    {
        if(Kotlet.stack.Count == 0)
            Errors.Error("stack is empty");
        
        return Kotlet.stack.Last();
    }
}
class PrintNode : Node
{
    public override void Run()
    {
        var s = GetLastStackItem().ToStr();
        WriteLine(s);
    }
}
class StackPush : Node
{
    TType type = new TType();

    public StackPush(TType type)
    {
        this.type = type;
    }

    public override void Run()
    {
        Kotlet.stack.Add(type);
    }
}
class Func : Node
{
    public Action function;

    public Func(Action function)
    {
        this.function = function;
    }
    public override void Run()
    {
        function();
    }
}