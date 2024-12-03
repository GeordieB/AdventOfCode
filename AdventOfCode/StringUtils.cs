namespace AdventOfCode;

public static class StringUtils
{
    public static int AsInt(this string value)
    {
        return int.TryParse(value, out var result) ? result : 0;
    }

    public static string ReplaceIgnoreCase(this string value, string oldValue, string newValue)
    {
        return value.Replace(oldValue.ToLower(), newValue).Replace(oldValue.ToUpper(), newValue);
    }
}