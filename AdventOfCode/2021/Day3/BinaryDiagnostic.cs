using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2021.Day3
{
    public class BinaryDiagnostic
    {
        public static void CalculatePowerConsumption()
        {
            var rawData = ReadFile();
            var calculation = ProcessData(rawData);

            calculation.CalculateRates();
            Console.WriteLine($"Power consumption is: {calculation.PowerConsumption}");
            Console.WriteLine($"Life support rating is: {calculation.LifeSupportRating}");
        }

        private static DiagnosticCalculation ProcessData(List<string> rawData)
        {
            var calculation = new DiagnosticCalculation();
            foreach (var raw in rawData)
            {
                calculation.Bits.Add(raw);
                for (int i = 0; i < raw.Length; i++)
                {
                    bool isExistingRow = calculation.BitRows.Count != 0 && calculation.BitRows.Count > i;
                    List<char> row = new List<char>();
                    if (isExistingRow)
                        row = calculation.BitRows.ElementAt(i);

                    row.Add(raw[i]);

                    if(!isExistingRow)
                        calculation.BitRows.Add(row);
                }
            }

            return calculation;
        }

        private static List<string> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_BINARY_DIAGNOSTIC).ToList();
        }
    }

    public class DiagnosticCalculation
    {
        public const char ONE = '1';
        public const char ZERO = '0';

        public List<List<char>> BitRows { get; set; } = new List<List<char>>();
        public List<string> Bits { get; set; } = new List<string>();
        public string Gamma { get; set; }
        public string Epsilon { get; set; }
        public string OxygenRationg { get; set; }
        public string CO2Rating { get; set; }
        public int PowerConsumption { get { return GetPowerConsumption(); } }
        public int LifeSupportRating { get { return GetLifeSupportRating(); } }

        public void CalculateRates()
        {
            var gamma = string.Empty;
            var epsilon = string.Empty;
            var o2RelatedBits = Bits.Select(x => x).ToList();
            var cO2RelatedBits = Bits.Select(x => x).ToList();

            var index = 0;
            foreach (var bitList in BitRows)
            {
                var zeroCount = bitList.Count(p => p == ZERO);
                var oneCount = bitList.Count(p => p == ONE);

                var gammaBit = zeroCount > oneCount ? ZERO : ONE;
                var epsilonBit = zeroCount < oneCount ? ZERO : ONE;

                gamma += gammaBit;
                epsilon += epsilonBit;

                if (o2RelatedBits.Count > 1)
                {
                    zeroCount = o2RelatedBits.Count(p => p[index] == ZERO);
                    oneCount = o2RelatedBits.Count(p => p[index] == ONE);
                    gammaBit = zeroCount > oneCount ? ZERO : ONE;
                    gammaBit = zeroCount == oneCount ? ONE : gammaBit;
                    o2RelatedBits = o2RelatedBits.Where(p => p[index] == gammaBit).ToList();
                }

                if (cO2RelatedBits.Count > 1)
                {
                    zeroCount = cO2RelatedBits.Count(p => p[index] == ZERO);
                    oneCount = cO2RelatedBits.Count(p => p[index] == ONE);
                    epsilonBit = zeroCount < oneCount ? ZERO : ONE;
                    epsilonBit = zeroCount == oneCount ? ZERO : epsilonBit;
                    cO2RelatedBits = cO2RelatedBits.Where(p => p[index] == epsilonBit).ToList();
                }

                index++;
            }

            Gamma = gamma;
            Epsilon = epsilon;
            OxygenRationg = o2RelatedBits.FirstOrDefault();
            CO2Rating = cO2RelatedBits.FirstOrDefault();
        }

        public int GetPowerConsumption()
        {
            if (string.IsNullOrEmpty(Gamma) || string.IsNullOrEmpty(Epsilon))
                return 0;

            var gammaAsInt = Convert.ToInt32(Gamma, 2);
            var epsilonAsInt = Convert.ToInt32(Epsilon, 2);

            return gammaAsInt * epsilonAsInt;
        }

        public int GetLifeSupportRating()
        {
            if (string.IsNullOrEmpty(OxygenRationg) || string.IsNullOrEmpty(CO2Rating))
                return 0;

            var o2AsInt = Convert.ToInt32(OxygenRationg, 2);
            var cO2AsInt = Convert.ToInt32(CO2Rating, 2);

            return o2AsInt * cO2AsInt;
        }
    }
}