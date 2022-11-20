namespace AdventOfCode;

public class FileUtils
{
    public static string[] ReadFile(string fileName)
    {
        return File.ReadAllLines($"{Const.FILEPATH}{fileName}");
    }
}