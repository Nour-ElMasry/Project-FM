using Domain.Enum;
using Domain.Exceptions;
using System;

namespace Domain.Entities;
public class PlayerStats
{
    public int Attacking { get; set; }
    public int PlayMaking { get; set; }
    public int Defending { get; set; }
    public int Goalkeeping { get; set; }

    public PlayerStats(int attacking, int playMaking, int defending, int goalkeeping)
    {
        if (attacking >= 100 || playMaking >= 100 || defending >= 100 || goalkeeping >= 100)
            throw new InvalidDataException("A player stat can't be larger than 99!");
        Attacking = attacking;
        PlayMaking = playMaking;
        Defending = defending;
        Goalkeeping = goalkeeping;
    }

    public PlayerStats(string playerRole)
    {
        GenerateStats(playerRole);
    }

    public void GenerateStats(string role)
    {
        Random rnd = new();
        if (!System.Enum.TryParse(role, out Roles pr))
        {
            Attacking = rnd.Next(10, 50);
            PlayMaking = rnd.Next(10, 50);
            Defending = rnd.Next(10, 50);
            Goalkeeping = rnd.Next(10, 50);
            return;
        }

        switch (pr)
        {
            case Roles.Attacker:
                Attacking = rnd.Next(75, 100);
                PlayMaking = rnd.Next(60, 80);
                Defending = rnd.Next(20, 50);
                Goalkeeping = rnd.Next(10, 20);
                break;
            case Roles.Midfielder:
                Attacking = rnd.Next(60, 80);
                PlayMaking = rnd.Next(75, 100);
                Defending = rnd.Next(20, 50);
                Goalkeeping = rnd.Next(10, 20);
                break;

            case Roles.Defender:
                Attacking = rnd.Next(20, 50);
                PlayMaking = rnd.Next(40, 70);
                Defending = rnd.Next(75, 100);
                Goalkeeping = rnd.Next(10, 20);
                break;
            case Roles.Goalkeeper:
                Attacking = rnd.Next(10, 20);
                PlayMaking = rnd.Next(30, 60);
                Defending = rnd.Next(10, 40);
                Goalkeeping = rnd.Next(75, 100);
                break;
            default:
                return;
        }

    }
}
