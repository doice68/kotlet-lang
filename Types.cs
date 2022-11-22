// base class for types
class TType
{
    public virtual string GetTypeName() => "null";

    public virtual bool GetAsBool()
    {
        Errors.Error("expected a bool got " + GetTypeName());
        return false;
    }
    public virtual string GetAsStr()
    {
        Errors.Error("expected a string got " + GetTypeName());
        return "";
    }
    public virtual float GetAsNum()
    {
        Errors.Error("not a number got " + GetTypeName());
        return 0;
    }
    public virtual string ToStr()
    {
        Errors.Error($"cannot convert {GetTypeName()} to string");
        return "";
    }
}
class Num : TType
{
    float value;

    public Num(float value)
    {
        this.value = value;
    }

    public override string GetTypeName() => "number";
    public override string ToStr() => value.ToString();
    
    public override float GetAsNum()
    {
        return value;
    }
}
class Str : TType
{
    string value = "";

    public Str(string value)
    {
        this.value = value;
    }
    public override string GetTypeName() => "string";
    public override string ToStr() => value;
    public override string GetAsStr() => value;
}