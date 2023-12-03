namespace AdventOfCode._2022.Day6;

public class PacketMarkers
{
    public static void FindPacketMarker()
    {
        var packet = ReadFile().FirstOrDefault();

        var marker = packet.Substring(0, 4);
        var index = 1;
        while(!IsAMarker(marker))
        {
            marker = packet.Substring(index, 4);
            index++;
        }

        Console.WriteLine($"The start-of-packet marker was found to be: {marker} at position {index + 3}");
    }

    public static void FindMessageMarker()
    {
        var packet = ReadFile().FirstOrDefault();

        var marker = packet.Substring(0, 14);
        var index = 1;
        while (!IsAMarker(marker))
        {
            marker = packet.Substring(index, 14);
            index++;
        }

        Console.WriteLine($"The start-of-message marker was found to be: {marker} at position {index + 13}");
    }

    private static bool IsAMarker(string marker)
    {
        var distinct = marker.ToList().Distinct();
        return marker.Length == distinct.Count();
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_PACKETMARKERS).ToList();
    }
}
