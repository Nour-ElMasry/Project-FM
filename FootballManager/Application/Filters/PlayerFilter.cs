using Domain.Entities;

namespace Application.Filters
{
    public class PlayerFilter
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public int MinYearOfBirth { get; set; }
        public int MaxYearOfBirth { get; set; }
        public long Team { get; set; }
        public string Position { get; set; }


        public bool isEmpty()
        {
            if (this.Team == 0 &&
                String.IsNullOrWhiteSpace(this.Name) &&
                String.IsNullOrWhiteSpace(this.Country) &&
                String.IsNullOrWhiteSpace(this.Position) &&
                this.MinYearOfBirth == 0 &&
                this.MaxYearOfBirth == 0)
                return true;

            return false;
        }

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
