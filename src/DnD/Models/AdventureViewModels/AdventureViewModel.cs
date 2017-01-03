using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.AdventureViewModels
{
    public class AdventureViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(Adventure.DescriptionLength)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        public DateTime? Date { get; set; }

        [Display(Name = "Previous Adventure")]
        public int? PreviousId { get; set; }

        [Display(Name = "Next Adventure")]
        public int? NextId { get; set; }

        [Display(Name = "Dungeon Master")]
        public string DungeonMasterId { get; set; }
    }
}
