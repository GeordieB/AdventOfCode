using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2020.Day1
{
    public class ReportRepair
    {
        public static void RepairReportDouble()
        {
            List<int> expenses = ReadFile();

            int fixedExpense = 0;
            for (int i = 0; i < expenses.Count; i++)
            {
                int first = expenses[i];
                foreach (int expense in expenses.Where(p => p != first))
                {
                    if (first + expense == 2020)
                    {
                        fixedExpense = first * expense;
                        break;
                    }
                }

                if (fixedExpense != 0)
                    break;
            }
            Console.WriteLine($"Fixed Expense: {fixedExpense}");
        }

        public static void RepairReportTriple()
        {
            List<int> expenses = ReadFile();

            int fixedExpense = 0;
            for (int i = 0; i < expenses.Count; i++)
            {
                int firstExpense = expenses[i];
                foreach (int secondExpense in expenses.Where(p => p != firstExpense))
                {
                    foreach (int thirdExpense in expenses.Where(p => p != firstExpense && p != secondExpense))
                    {
                        if (firstExpense + secondExpense + thirdExpense == 2020)
                        {
                            fixedExpense = firstExpense * secondExpense * thirdExpense;
                            break;
                        }
                    }
                    if (fixedExpense != 0)
                        break;
                }

                if (fixedExpense != 0)
                    break;
            }
            Console.WriteLine($"Fixed Expense: {fixedExpense}");
        }

        private static List<int> ReadFile()
        {
            return FileUtils.ReadFile(Const.FILE_REPORT_REPAIR)
                .Select(p => p.AsInt()).ToList();
        }
    }
}