﻿using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class UserPostDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Country { get; set; }

        [Required]
        [StringLength(10)]
        public string DateOfBirth { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(4)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }
    }
}
