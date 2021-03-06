﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class Adventure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public const int DescriptionLength = 5000;
        [MaxLength(DescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int? PreviousId { get; set; }
        public Adventure Previous { get; set; }

        public int? NextId { get; set; }
        public Adventure Next { get; set; }

        public string DungeonMasterId { get; set; }
        [Required]
        [Display(Name = "Dungeon Master")]
        public ApplicationUser DungeonMaster { get; set; }

        [InverseProperty("Adventure")]
        public List<AdventureParticipation> Adventurers { get; set; }

        public string DisplayName => $"{Name} ({Date:d})";
    }
}
