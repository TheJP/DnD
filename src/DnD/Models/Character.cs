using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class Character
    {
        public int CharacterId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }
    }
}
