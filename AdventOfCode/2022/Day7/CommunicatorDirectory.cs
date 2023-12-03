namespace AdventOfCode._2022.Day7;

public class CommunicatorDirectory
{
    public static void PeruseDirectory()
    {
        var commands = ReadFile();
        var directory = new SystemDirectory();

        var isRootDirectory = true;
        var currentDirectoryName = string.Empty;
        var previousDirectoryName = string.Empty;

        foreach (var command in commands)
        {
            var subCommands = command.Trim().Split(" ");
            if (subCommands[0] == "$")
            {
                if (subCommands[1] == "cd")
                {
                    previousDirectoryName = currentDirectoryName;
                    currentDirectoryName = subCommands[2];
                    if (directory.Name != currentDirectoryName)
                    {
                        if (currentDirectoryName != "/")
                        {
                            if (isRootDirectory)
                            {
                                if (directory.Directories.All(p => p.Name != currentDirectoryName))
                                {
                                    directory.Directories.Add(new SystemDirectory
                                    {
                                        Name = currentDirectoryName
                                    });
                                }
                                isRootDirectory = false;
                            }
                            else
                            {
                                var dir = directory.Directories.First(p => p.Name == previousDirectoryName);
                                if (dir.Directories.All(p => p.Name != currentDirectoryName))
                                {
                                    dir.Directories.Add(new SystemDirectory
                                    {
                                        Name = currentDirectoryName
                                    });
                                }
                            }
                        }
                        else
                        {
                            directory.Name = currentDirectoryName;
                        }
                    }

                    continue;
                }
                else if (subCommands[1] == "ls")
                    continue;
            }
            
            
            if (subCommands[0] == "dir")
            {
                if (isRootDirectory)
                {
                    directory.Directories.Add(new SystemDirectory
                    {
                        Name = subCommands[1]
                    });
                }
                else
                {
                    var dir = directory.Directories.First(p => p.Name == currentDirectoryName);
                    dir.Directories.Add(new SystemDirectory
                    {
                        Name = subCommands[1]
                    });
                }
            }
            else if (int.TryParse(subCommands[0], out var fileSize))
            {
                if (isRootDirectory)
                {
                    directory.Files.Add(new SystemFile
                    {
                        Name = subCommands[1],
                        FileSize = fileSize
                    });
                }
                else
                {
                    var dir = directory.Directories.First(p => p.Name == currentDirectoryName);
                    dir.Files.Add(new SystemFile
                    {
                        Name = subCommands[1],
                        FileSize = fileSize
                    });
                }
            }
        }
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_COMMUNICATORDIRECTORY).ToList();
    }
}
