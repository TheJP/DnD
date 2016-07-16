using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    /// <summary>
    /// One instance of this class is a single gold earning or loss.
    /// A character normally has multiple of those.
    /// </summary>
    public class Gold
    {
        public int Id { get; set; }

        public int? LootFromId { get; set; }
        public AdventureParticipation LootFrom { get; set; }

        public int CharacterId { get; set; }
        [Required]
        public Character Character { get; set; }

        /// <summary>Amount of gold that was earned / lost.</summary>
        [Required]
        public int Value { get; set; }

        public string Description { get; set; }
    }
}
