namespace Okex.Net.Helpers;

public static class OkexExtensions
{
    /// <summary>
    /// Validate the string is a valid spot symbol.
    /// </summary>
    /// <param name="symbol">string to validate</param>
    public static string ValidateSymbol(this string symbol, string messagePrefix = "", string messageSuffix = "")
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}Symbol is not provided{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        // symbol = symbol.ToLower(CultureInfo.InvariantCulture);
        if (!Regex.IsMatch(symbol, "^(([a-z]|[A-Z]|-|[0-9]){4,})$"))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{symbol} is not a valid Okex Symbol. Should be [QuoteCurrency]-[BaseCurrency], e.g. ETH-BTC{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        return symbol;
    }

    /// <summary>
    /// Validate the string is a valid spot currency.
    /// </summary>
    /// <param name="currency">Currency</param>
    /// <returns></returns>
    public static string ValidateCurrency(this string currency, string messagePrefix = "", string messageSuffix = "")
    {
        if (string.IsNullOrEmpty(currency))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}Symbol is not provided{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        if (!Regex.IsMatch(currency, "^(([a-z]|[A-Z]){2,})$"))
            throw new ArgumentException($"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{currency} is not a valid Okex Currency. Should be [Currency] only, e.g. BTC{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");

        return currency;
    }

    public static void ValidateStringLength(this string @this, string argumentName, int minLength, int maxLength, string messagePrefix = "", string messageSuffix = "")
    {
        if (@this.Length < minLength || @this.Length > maxLength)
            throw new ArgumentException(
                $"{messagePrefix}{(messagePrefix.Length > 0 ? " " : "")}{@this} not allowed for parameter {argumentName}, Min Length: {minLength}, Max Length: {maxLength}{(messageSuffix.Length > 0 ? " " : "")}{messageSuffix}");
    }

    #region Null
    public static bool IsNull(this object @this)
    {
        return (@this == null || @this.GetType() == typeof(DBNull));
    }

    public static bool IsNotNull(this object @this)
    {
        return !@this.IsNull();
    }
    #endregion

    #region ToStr
    public static string ToStr(this object @this, bool nullToEmpty = true)
    {
        bool isNull = @this == null ? true : false;
        bool isDBNull = @this != null && @this.GetType() == typeof(DBNull) ? true : false;

        if (isNull)
            return nullToEmpty ? string.Empty : null;
        else if (isDBNull)
            return nullToEmpty ? string.Empty : null;
        else
            return @this?.ToString();
    }
    #endregion

    #region ToNumber
    public static int ToInt(this object @this)
    {
        int result = 0;
        if (@this.IsNotNull()) int.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static long ToLong(this object @this)
    {
        long result = 0;
        if (@this.IsNotNull()) long.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static double ToDouble(this object @this)
    {
        if (@this == null) return 0.0;

        double result = 0.0;
        if (@this.IsNotNull()) double.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    public static double? ToDoubleNullable(this object @this)
    {
        if (@this == null) return null;

        double result = 0.0;
        if (@this.IsNotNull()) double.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static decimal ToDecimal(this object @this)
    {
        if (@this == null) return 0;

        decimal result = 0.0m;
        if (@this.IsNotNull()) decimal.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    public static decimal? ToDecimalNullable(this object @this)
    {
        if (@this == null) return null;

        decimal result = 0.0m;
        if (@this.IsNotNull()) decimal.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }

    public static float ToFloat(this object @this)
    {
        if (@this == null) return 0;

        float result = 0;
        if (@this.IsNotNull()) float.TryParse(@this.ToStr(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result);
        return result;
    }
    #endregion

    #region Epoch TimeStamp
    public static DateTime FromUnixTimeSeconds(this int @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(@this);
    }

    public static DateTime FromUnixTimeSeconds(this long @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(@this);
    }

    public static long ToUnixTimeSeconds(this DateTime @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((@this - epoch).TotalSeconds);
    }
    public static DateTime FromUnixTimeMilliSeconds(this long @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddSeconds(@this / 1000);
    }

    public static long ToUnixTimeMilliSeconds(this DateTime @this)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64((@this - epoch).TotalSeconds) * 1000 + @this.Millisecond;
    }
    #endregion

    #region ISO 8601 DateTime
    public static string DateTimeToIso8601String(this DateTime @this)
    {
        return @this.ToString(OkexGlobals.OkexDatetimeFormat);
    }

    public static DateTime Iso8601StringToDateTime(this string @this)
    {
        return DateTime.ParseExact(@this, OkexGlobals.OkexDatetimeFormat, CultureInfo.InvariantCulture);
    }
    #endregion

    #region String IsOneOf
    public static bool IsOneOf(this string @this, params string[] values)
    {
        foreach (var v in values)
        {
            if (@this == v)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region String IsNotOneOf
    public static bool IsNotOneOf(this string @this, params string[] values)
    {
        return !@this.IsOneOf(values);
    }

    #endregion

    #region Integer IsOneOf
    public static bool IsOneOf(this int @this, params int[] values)
    {
        foreach (var v in values)
        {
            if (@this == v)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Integer IsNotOneOf
    public static bool IsNotOneOf(this int @this, params int[] values)
    {
        return !@this.IsOneOf(values);
    }

    #endregion

    public static bool IsIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) != -1;
    }

    public static bool IsNotIn<T>(this T @this, params T[] values)
    {
        return Array.IndexOf(values, @this) == -1;
    }
}