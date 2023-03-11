using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Career
    {
        [Key]
        public int CareerId { get; set; }
        public String CareerName { get; set; }
        public User CareerUser { get; set; }
        public RealManager CareerManager { get; set; }
        public List<League> Leagues { get; set; } = new();
        public Career(String careerName, User careerUser)
        {
            CareerName = careerName;
            CareerUser = careerUser;
        }
        public Career()
        {
        }
    }
}
