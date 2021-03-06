﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class AdventureParticipation
    {
        public int Id { get; set; }

        public int AdventureId { get; set; }
        [Required]
        public Adventure Adventure { get; set; }

        public int AdventurerId { get; set; }
        [Required]
        public Character Adventurer { get; set; }

        [InverseProperty("LootFrom")]
        public List<Gold> GoldLoot { get; set; }

        [InverseProperty("LootFrom")]
        public List<Experience> ExperienceLoot { get; set; }
    }
}
