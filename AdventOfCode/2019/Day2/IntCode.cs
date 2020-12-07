using System;

namespace AdventOfCode._2019.Day2
{
    public class IntCode
    {
        private const int ADD = 1;
        private const int MULTIPLY = 2;
        private const int HALT = 99;
        private const int DESIRED_RESULT = 19690720;

        private static string[] _values;

        public static void RunComputer()
        {
            _values = FileUtils.ReadFile(Const.FILE_INTCODE);
            _values = _values[0].Split(',');

            Run();
        }

        public static void DetermineNounAndVerb()
        {
            string[] initialValues = FileUtils.ReadFile(Const.FILE_INTCODE);
            initialValues = initialValues[0].Split(',');
            _values = new string[initialValues.Length];

            int answer = 0;

            for (int noun = 0; noun < HALT; noun++)
            {
                for (int verb = 0; verb < HALT; verb++)
                {
                    Array.Copy(initialValues, _values, initialValues.Length);
                    _values[1] = noun.ToString();
                    _values[2] = verb.ToString();

                    Run();

                    int result = _values[0].AsInt();

                    Console.WriteLine($"Noun: {noun}");
                    Console.WriteLine($"Noun: {verb}");

                    if (result == DESIRED_RESULT)
                    {
                        answer = (100 * noun) + verb;
                        break;
                    }
                }
                if (answer != 0)
                    break;
            }
            Console.WriteLine($"Answer: {answer}");
        }

        public static void Run()
        {
            int opCode = 0;
            int index = 0;

            while (opCode != HALT)
            {
                opCode = _values[index].AsInt();
                switch (opCode)
                {
                    case ADD:
                        Add(index);
                        break;
                    case MULTIPLY:
                        Multiply(index);
                        break;
                    case HALT:
                        break;
                }

                index += 4;
            }

            Console.WriteLine($"IntCode: {string.Join(',', _values)}\n");
            Console.WriteLine($"Value at position 0: {_values[0]}");
        }

        private static void Add(int index)
        {
            FetchIndex(index, out int first, out int second, out int indexOfResult);
            int result = first + second;
            UpdateIndex(indexOfResult, result);
        }

        private static void Multiply(int index)
        {
            FetchIndex(index, out int first, out int second, out int indexOfResult);
            int result = first * second;
            UpdateIndex(indexOfResult, result);
        }

        private static void FetchIndex(int index, out int first,
            out int second, out int indexOfResult)
        {
            int indexOfFirst = _values[index + 1].AsInt();
            int indexOfSecond = _values[index + 2].AsInt();
            indexOfResult = _values[index + 3].AsInt();

            first = _values[indexOfFirst].AsInt();
            second = _values[indexOfSecond].AsInt();
        }

        private static void UpdateIndex(int index, int result)
        {
            _values[index] = result.ToString();
        }
    }
}