using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class Character
    {
        /// <summary>InitialLife + InitialMana has to be equal to this number. (Except for dragons, that start with predefined stats.)</summary>
        public const int InitialLifePlusMana = 15;

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int InitialLife { get; set; }

        [Required]
        public int InitialMana { get; set; }

        public string OwnerId { get; set; }
        [Required]
        public ApplicationUser Owner { get; set; }

        public int RaceId { get; set; }
        [Required]
        public Race Race { get; set; }

        [InverseProperty("Adventurer")]
        public List<AdventureParticipation> Adventures { get; set; }

        [InverseProperty("Character")]
        public List<Gold> Gold { get; set; }

        [InverseProperty("Character")]
        public List<Experience> Experience { get; set; }
    }
}
