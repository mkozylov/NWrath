namespace NWrath.Synergy.Formatting
{
    public interface IStringSerializer<TObj>
    {
        string Serialize(TObj instance);
    }
}