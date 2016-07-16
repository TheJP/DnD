using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.CharacterViewModels
{
    public class CharacterViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [HiddenInput]
        public string Gender { get; set; }

        [Display(Name = "Race")]
        public int RaceId { get; set; }
    }
}
