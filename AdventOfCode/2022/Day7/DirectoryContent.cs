namespace AdventOfCode._2022.Day7;

//public class DirectoryContent
//{
//    public string Name { get; set; }
//    public List<SystemDirectory> Directories { get; set; } = new();
//    public List<SystemFile> Files { get; set; } = new();
//}

public class SystemDirectory
{
    public string Name { get; set; }
    public List<SystemDirectory> Directories { get; set; } = new();
    public List<SystemFile> Files { get; set; } = new();
}

public class SystemFile
{
    public string Name { get; set; }
    public int FileSize { get; set; }
}