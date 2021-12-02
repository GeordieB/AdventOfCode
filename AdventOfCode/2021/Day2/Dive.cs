using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day2
{
    public class Dive
    {
        public static void CalculatePosition()
        {
            var rawData = ReadFile();
            var instructions = ProcessData(rawData);

            var position = CalculateSimplePosition(instructions);

            Console.WriteLine($"Final Simple Horizontal Position: {position.HorizontalPosition}");
            Console.WriteLine($"Final Simple Depth: {position.Depth}");
            Console.WriteLine($"Final Simple Multiplication: {position.Multiplication}");

            var aimedPosition = CalculateAimedPosition(instructions);

            Console.WriteLine($"\nFinal Aimed Horizontal Position: {aimedPosition.HorizontalPosition}");
            Console.WriteLine($"Final Aimed Depth: {aimedPosition.Depth}");
            Console.WriteLine($"Final Aim: {aimedPosition.Aim}");
            Console.WriteLine($"Final Aimed Multiplication: {aimedPosition.Multiplication}");
        }

        private static SimplePosition CalculateSimplePosition(List<DivingInstruction> instructions)
        {
            var position = new SimplePosition();

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case DivingDirection.Forward:
                        position.HorizontalPosition += instruction.Value;
                        break;
                    case DivingDirection.Down:
                        position.Depth += instruction.Value;
                        break;
                    case DivingDirection.Up:
                        position.Depth -= instruction.Value;
                        break;
                }
            }

            return position;
        }

        private static AimedPosition CalculateAimedPosition(List<DivingInstruction> instructions)
        {
            var position = new AimedPosition();

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case DivingDirection.Forward:
                        position.HorizontalPosition += instruction.Value;
                        position.Depth += position.Aim * instruction.Value;
                        break;
                    case DivingDirection.Down:
                        position.Aim += instruction.Value;
                        break;
                    case DivingDirection.Up:
                        position.Aim -= instruction.Value;
                        break;
                }
            }

            return position;
        }

        private static List<DivingInstruction> ProcessData(List<string> data)
        {
            var instructions = new List<DivingInstruction>();
            foreach (var raw in data)
            {
                if (string.IsNullOrEmpty(raw))
                    continue;

                var result = raw.Split(" ");
                var instruction = ProcessData(result);
                instructions.Add(instruction);
            }

            return instructions;
        }

        private static DivingInstruction ProcessData(string[] data)
        {
            var instruction = new DivingInstruction()
            {
                Value = int.Parse(data[1])
            };
            switch (data[0])
            {
                case "forward":
                    instruction.Direction = DivingDirection.Forward;
                    break;
                case "down":
                    instruction.Direction = DivingDirection.Down;
                    break;
                case "up":
                    instruction.Direction = DivingDirection.Up;
                    break;
            }

            return instruction;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_DIVE).ToList();
        }
    }

    public class DivingInstruction
    {
        public DivingDirection Direction { get; set; }
        public int Value { get; set; }
    }

    public class SimplePosition
    {
        public int HorizontalPosition { get; set; }
        public int Depth { get; set; }
        public int Multiplication { get { return HorizontalPosition * Depth; } }
    }

    public class AimedPosition : SimplePosition
    {
        public int Aim { get; set; }
    }
}