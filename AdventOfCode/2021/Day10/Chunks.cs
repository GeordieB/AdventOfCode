namespace AdventOfCode._2021.Day10;

public class Chunks
{
    public static void FindCorruptedChunks()
    {
        var rawData = ReadFile();

        var erroredChunks = new Dictionary<int, (char expected, char recieved)>();
        var incompleteChunks = new List<string>();

        for (int i = 0; i < rawData.Count; i++)
        {
            var chunk = rawData[i];
            var errorCount = erroredChunks.Count;
            while (chunk.Length > 0)
            {
                chunk = ProcessChunk(chunk, i, erroredChunks, incompleteChunks);
                if (erroredChunks.Count > errorCount)
                    break;
            }
        }

        DisplayCorruptedLinesAndPoints(erroredChunks);

        var completedChunks = new Dictionary<int, string>();
        for (int i = 0; i < incompleteChunks.Count; i++)
        {
            var chunk = incompleteChunks[i];
            ProcessIncompleteChunk(chunk, i, completedChunks);
        }

        DisplayCompletedLinesAndPoints(completedChunks);
    }

    private static void DisplayCompletedLinesAndPoints(Dictionary<int, string> completedChunks)
    {
        var completedChunkPoints = new List<long>();
        foreach (var completedChunk in completedChunks)
        {
            long totalPoints = 0;
            foreach (var value in completedChunk.Value)
            {
                switch (value)
                {
                    case ')':
                        totalPoints = (totalPoints * 5) + 1;
                        break;
                    case ']':
                        totalPoints = (totalPoints * 5) + 2;
                        break;
                    case '}':
                        totalPoints = (totalPoints * 5) + 3;
                        break;
                    case '>':
                        totalPoints = (totalPoints * 5) + 4;
                        break;
                }
            }
            completedChunkPoints.Add(totalPoints);

            Console.WriteLine($"Complete by adding: {completedChunk.Value} with a total points of: {totalPoints}");
        }
        completedChunkPoints = completedChunkPoints.OrderBy(x => x).ToList();
        var winner = completedChunkPoints.ElementAt((int)Math.Ceiling((completedChunkPoints.Count - 1) / 2.0));
        Console.WriteLine($"The middle score is: {winner}");
    }

    private static void DisplayCorruptedLinesAndPoints(Dictionary<int, (char expected, char recieved)> erroredChunks)
    {
        var totalPoints = 0;
        foreach (var error in erroredChunks)
        {
            var illegalCharacter = error.Value.recieved;
            switch (illegalCharacter)
            {
                case ')':
                    totalPoints += 3;
                    break;
                case ']':
                    totalPoints += 57;
                    break;
                case '}':
                    totalPoints += 1197;
                    break;
                case '>':
                    totalPoints += 25137;
                    break;
            }
            Console.WriteLine($"Error at index: {error.Key}. Expected char {error.Value.expected}, recieved char {illegalCharacter}");
        }
        Console.WriteLine($"Total Points: {totalPoints}\n");
    }

    private static string ProcessChunk(string chunk, int index,
        Dictionary<int, (char expected, char recieved)> erroredChunks,
        List<string> incompleteChunks)
    {
        for (int j = 1; j < chunk.Length; j++)
        {
            switch (chunk[j])
            {
                case ')':
                    if (ValidateCharacter(chunk[j - 1], '(', ')', index, erroredChunks))
                    {
                        return BuildShortenedChunk(chunk, j);
                    }
                    break;
                case ']':
                    if (ValidateCharacter(chunk[j - 1], '[', ']', index, erroredChunks))
                    {
                        return BuildShortenedChunk(chunk, j);
                    }
                    break;
                case '}':
                    if (ValidateCharacter(chunk[j - 1], '{', '}', index, erroredChunks))
                    {
                        return BuildShortenedChunk(chunk, j);
                    }
                    break;
                case '>':
                    if (ValidateCharacter(chunk[j - 1], '<', '>', index, erroredChunks))
                    {
                        return BuildShortenedChunk(chunk, j);
                    }
                    break;
            }
        }

        if(!erroredChunks.ContainsKey(index))
            incompleteChunks.Add(chunk);
        return string.Empty;
    }

    private static bool ValidateCharacter(char previousCharacter,
        char openingCharacter, char receivedCharacter, int index,
        Dictionary<int, (char expected, char recieved)> erroredChunks)
    {
        if (previousCharacter != openingCharacter)
        {
            var expectedClosingCharacter = ' ';
            switch (previousCharacter)
            {
                case '(':
                    expectedClosingCharacter = ')';
                    break;
                case '[':
                    expectedClosingCharacter = ']';
                    break;
                case '{':
                    expectedClosingCharacter = '}';
                    break;
                case '<':
                    expectedClosingCharacter = '>';
                    break;
            }
            erroredChunks.TryAdd(index, (expectedClosingCharacter, receivedCharacter));
            return false;
        }
        return true;
    }

    private static string BuildShortenedChunk(string chunk, int index)
    {
        if(index > 1)
            return chunk.Substring(0, index - 1) + chunk.Substring(index + 1);
        return chunk.Substring(index + 1);
    }

    private static void ProcessIncompleteChunk(string chunk, int index,
        Dictionary<int, string> completedChunks)
    {
        var completedChunk = string.Empty;
        for (int j = chunk.Length - 1; j >= 0; j--)
        {
            switch (chunk[j])
            {
                case '(':
                    completedChunk += ")";
                    break;
                case '[':
                    completedChunk += "]";
                    break;
                case '{':
                    completedChunk += "}";
                    break;
                case '<':
                    completedChunk += ">";
                    break;
            }
        }
        completedChunks.TryAdd(index, completedChunk);
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_CHUNKS).ToList();
    }
}
