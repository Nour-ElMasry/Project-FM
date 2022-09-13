using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public abstract class Manager
{
    [Key]
    public long ManagerId { get; set; }

    [ForeignKey("ManagerPersonId")]
    public Person ManagerPerson { get; set; }

    [ForeignKey("CurrentTeamId")]
    public Team CurrentTeam { get; set; }
}

