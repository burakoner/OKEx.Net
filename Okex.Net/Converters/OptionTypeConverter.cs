namespace Okex.Net.Converters;

internal class OptionTypeConverter : BaseConverter<OkexOptionType>
{
    public OptionTypeConverter() : this(true) { }
    public OptionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkexOptionType, string>> Mapping => new List<KeyValuePair<OkexOptionType, string>>
    {
        new KeyValuePair<OkexOptionType, string>(OkexOptionType.Call, "C"),
        new KeyValuePair<OkexOptionType, string>(OkexOptionType.Put, "P"),
    };
}