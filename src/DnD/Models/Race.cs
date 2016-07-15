using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class Race
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Lore { get; set; }

        public List<RaceAttribute> Attributes { get; set; }

        public List<Character> Characters { get; set; }
    }
}
