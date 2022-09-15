namespace FootballManagerAPI.Filters
{
    public class PlayerFilter
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public int MinYearOfBirth { get; set; }
        public int MaxYearOfBirth { get; set; }
        public long TeamId { get; set; }
        public string Position { get; set; }

        public bool IsValidYearRange()
        {
            if (MinYearOfBirth == 0)
                return true;

            if (MaxYearOfBirth == 0)
                return true;

            return MaxYearOfBirth >= MinYearOfBirth;
        }
    }
}
