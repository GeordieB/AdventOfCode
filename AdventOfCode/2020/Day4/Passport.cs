namespace AdventOfCode._2020.Day4
{
    public class Passport
    {
        public int BirthYear { get; set; }
        public int IssueYear { get; set; }
        public int ExpirationYear { get; set; }
        public int Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportID { get; set; }
        public string CountryID { get; set; }
        public bool IsValid
        {
            get
            {
                return BirthYear != 0 && IssueYear != 0 && ExpirationYear != 0
                     && Height != 0 && !string.IsNullOrEmpty(HairColor) && !string.IsNullOrEmpty(EyeColor)
                     && !string.IsNullOrEmpty(PassportID);
            }
        }

        public Passport(string rawData)
        {
            string[] values = rawData.Trim().Split(' ');
            foreach (string data in values)
            {
                string[] param = data.Split(':');
                string value = param[1];
                switch (param[0])
                {
                    case "byr":
                        BirthYear = value.AsInt();
                        break;
                    case "iyr":
                        IssueYear = value.AsInt();
                        break;
                    case "eyr":
                        ExpirationYear = value.AsInt();
                        break;
                    case "hgt":
                        Height = value.Substring(0, value.Length - 2).AsInt();
                        break;
                    case "hcl":
                        HairColor = value;
                        break;
                    case "ecl":
                        EyeColor = value;
                        break;
                    case "pid":
                        PassportID = value;
                        break;
                    case "cid":
                        CountryID = value;
                        break;
                }
            }
        }
    }
}