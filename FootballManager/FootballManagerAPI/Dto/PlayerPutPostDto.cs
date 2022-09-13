﻿using System.ComponentModel.DataAnnotations;

namespace FootballManagerAPI.Dto
{
    public class PlayerPutPostDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        public string Country { get; set; }

        [Required]
        [StringLength(10)]
        public string DateOfBirth { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(4)]
        public string Position { get; set; }
    }
}
