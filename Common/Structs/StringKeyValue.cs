namespace NWrath.Synergy.Common.Structs
{
    public class StringKeyValue
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public StringKeyValue()
        {
        }

        public StringKeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}