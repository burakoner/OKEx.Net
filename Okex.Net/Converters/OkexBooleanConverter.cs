namespace Okex.Net.Converters;

internal class OkexBooleanConverter : BaseConverter<bool>
{
    public OkexBooleanConverter() : this(true) { }
    public OkexBooleanConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<bool, string>> Mapping => new List<KeyValuePair<bool, string>>
    {
        new KeyValuePair<bool, string>(true, "1"),
        new KeyValuePair<bool, string>(true, "True"),
        new KeyValuePair<bool, string>(true, "true"),
        new KeyValuePair<bool, string>(true, "Yes"),
        new KeyValuePair<bool, string>(true, "yes"),
        new KeyValuePair<bool, string>(true, "T"),
        new KeyValuePair<bool, string>(true, "t"),
        new KeyValuePair<bool, string>(true, "Y"),
        new KeyValuePair<bool, string>(true, "y"),
        new KeyValuePair<bool, string>(false, "0"),
        new KeyValuePair<bool, string>(false, "False"),
        new KeyValuePair<bool, string>(false, "false"),
        new KeyValuePair<bool, string>(false, "No"),
        new KeyValuePair<bool, string>(false, "no"),
        new KeyValuePair<bool, string>(false, "F"),
        new KeyValuePair<bool, string>(false, "f"),
        new KeyValuePair<bool, string>(false, "N"),
        new KeyValuePair<bool, string>(false, "n"),
    };
}