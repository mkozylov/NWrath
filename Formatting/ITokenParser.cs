using NWrath.Synergy.Formatting.Structs;

namespace NWrath.Synergy.Formatting
{
    public interface ITokenParser
    {
        string KeyPattern { get; set; }

        Token[] Parse(string template);
    }
}