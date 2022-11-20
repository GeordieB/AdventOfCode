namespace AdventOfCode;

public class Search
{
    /// <summary>
    /// This is assuming ints is an ordered array
    /// </summary>
    /// <param name="ints"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool ExistsUsingWhile(int[] ints, int value)
    {
        int[] temp = ints;
        bool found = false;
        int index = ints.Length / 2;
        int count = 0;

        while (!found)
        {
            count++;
            int result = temp[index];
            if (result == value)
            {
                found = true;
                break;
            }

            if (temp.Length == 1)
                break;
            if (result > value)
                temp = temp.Take(index).ToArray();
            if (result < value)
                temp = temp.Skip(index).ToArray();

            index = temp.Length / 2;
        }

        Console.WriteLine($"Value found: {found}, in {count} steps");
        return found;
    }

    /// <summary>
    /// This is assuming ints is an ordered array
    /// </summary>
    /// <param name="ints"></param>
    /// <param name="value"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static bool ExistsUsingRecursion(int[] ints, int value, int count = 0)
    {
        bool found = false;
        int index = ints.Length / 2;

        count++;
        int result = ints[index];
        if (result == value)
        {
            found = true;
            Console.WriteLine($"Value found: {found}, in {count} steps");
            return found;
        }

        if (ints.Length == 1)
        {
            Console.WriteLine($"Value found: {found}, in {count} steps");
            return false;
        }

        if (result > value)
            return ExistsUsingRecursion(ints.Take(index).ToArray(), value, count);

        if (result < value)
            return ExistsUsingRecursion(ints.Skip(index).ToArray(), value, count);

        return false;
    }

    public static int[] InitArray(int size)
    {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = i;
        }

        return arr;
    }
}