namespace AdventOfCode._2022.Day5;

public class SupplyStacks
{
    public static void StackMover9000()
    {
        var rawData = ReadFile();
        Dictionary<int, Stack<string>> stacks;
        List<string> rawCrateData, moves;
        int numberOfStacks;

        InitializeData(rawData, out stacks, out rawCrateData, out moves, out numberOfStacks);

        InitializeStacks(stacks, rawCrateData, numberOfStacks);

        PerformStacker9000Moves(stacks, moves);

        var topStacks = string.Empty;
        foreach (var crates in stacks.Values)
        {
            topStacks += crates.Pop();
        }

        Console.WriteLine($"The top stacks are: {topStacks}");
    }

    public static void StackMover9001()
    {
        var rawData = ReadFile();
        Dictionary<int, Stack<string>> stacks;
        List<string> rawCrateData, moves;
        int numberOfStacks;

        InitializeData(rawData, out stacks, out rawCrateData, out moves, out numberOfStacks);

        InitializeStacks(stacks, rawCrateData, numberOfStacks);

        PerformStacker9001Moves(stacks, moves);

        var topStacks = string.Empty;
        foreach (var crates in stacks.Values)
        {
            topStacks += crates.Pop();
        }

        Console.WriteLine($"The top stacks are: {topStacks}");
    }

    private static void InitializeData(List<string> rawData, out Dictionary<int, Stack<string>> stacks, out List<string> rawCrateData, out List<string> moves, out int numberOfStacks)
    {
        stacks = new Dictionary<int, Stack<string>>();
        rawCrateData = rawData.Where(x => x.Contains("[")).ToList();
        moves = rawData.Where(x => !x.Contains("[") && !string.IsNullOrEmpty(x)).ToList();
        numberOfStacks = moves.First().Split(' ').Count(p => !string.IsNullOrEmpty(p));
        moves.RemoveAt(0);
    }

    private static void PerformStacker9000Moves(Dictionary<int, Stack<string>> stacks, List<string> moves)
    {
        foreach (var move in moves)
        {
            var instructions = move.Split(" ");
            var numToMove = instructions[1].AsInt();
            var originalStack = instructions[3].AsInt() - 1;
            var finalStack = instructions[5].AsInt() - 1;

            for (int i = 0; i < numToMove; i++)
            {
                var crateToMove = stacks[originalStack].Pop();
                stacks[finalStack].Push(crateToMove);
            }
        }
    }

    private static void PerformStacker9001Moves(Dictionary<int, Stack<string>> stacks, List<string> moves)
    {
        foreach (var move in moves)
        {
            var instructions = move.Split(" ");
            var numToMove = instructions[1].AsInt();
            var originalStack = instructions[3].AsInt() - 1;
            var finalStack = instructions[5].AsInt() - 1;

            var tempStack = new Stack<string>();
            for (int i = 0; i < numToMove; i++)
            {
                var crateToMove = stacks[originalStack].Pop();
                tempStack.Push(crateToMove);
            }
            for (int i = 0; i < numToMove; i++)
            {
                var crateToMove = tempStack.Pop();
                stacks[finalStack].Push(crateToMove);
            }
        }
    }

    private static void InitializeStacks(Dictionary<int, Stack<string>> stacks, List<string> rawCrateData, int numberOfStacks)
    {
        for (int i = 0; i < numberOfStacks; i++)
        {
            var stack = new Stack<string>();
            var crates = new List<string>();
            foreach (var rawCrate in rawCrateData)
            {
                var crate = rawCrate[(i * 3) + i + 1];
                if (!crate.Equals(' '))
                    crates.Add(crate.ToString());
            }

            crates.Reverse();
            foreach (var crate in crates)
            {
                stack.Push(crate);
            }
            stacks[i] = stack;
        }
    }

    private static List<string> ReadFile()
    {
        return FileUtils.ReadFile(Const.FILE_SUPPLYSTACKS).ToList();
    }
}
