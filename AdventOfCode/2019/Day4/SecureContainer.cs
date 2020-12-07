using System;
using System.Collections.Generic;

namespace AdventOfCode._2019.Day4
{
    public class SecureContainer
    {
        public const int VALID_LENGTH = 6;

        private static string[] _passwords;
        private static List<int> _validPasswords = new List<int>();
        private static bool _useExtraCriteria;

        public static void ValidatePasswords()
        {
            _passwords = FileUtils.ReadFile(Const.FILE_SECURE_CONTAINER_PASSWORDS);
            _useExtraCriteria = true;

            Run();

            foreach (var valid in _validPasswords)
            {
                Console.WriteLine($"valid password: {valid}");
            }
            Console.WriteLine($"# of valid passwords: {_validPasswords.Count}");
        }

        private static void Run()
        {
            foreach (string range in _passwords)
            {
                string[] toValidate = range.Split('-');
                int start = toValidate[0].AsInt();
                int end = toValidate.Length > 1 ? toValidate[1].AsInt() : start + 1;

                for (int i = start; i < end; i++)
                {
                    if (Valid(i.ToString()))
                        _validPasswords.Add(i);
                }
            }
        }

        public static bool Valid(string password)
        {
            if (password.Length != VALID_LENGTH)
                return false;

            bool sameAdjacentDigits = false;

            var chars = password.ToCharArray();

            for (int i = 1; i < chars.Length; i++)
            {
                //if (!sameAdjacentDigits) {
                    sameAdjacentDigits = chars[i] == chars[i - 1];
                    if(i > 1)
                    {
                        sameAdjacentDigits = sameAdjacentDigits && chars[i] != chars[i - 2];
                    }
                //}
                //if (sameAdjacentDigits && i > 1 && chars[i] == chars[i - 2])
                //{
                //    return false;
                //}

                if (chars[i] < chars[i - 1])
                    return false;
            }

            return sameAdjacentDigits;
        }
    }
}