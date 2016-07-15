using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class DnDAttribute
    {
        public enum LevelUp
        {
            /// <summary>Attribute, which has custom logic for levelups.</summary>
            Custom,

            /// <summary>Attribute, which can be improved with character points. (Normally a character gets 6 of these per level.)</summary>
            WithCharacterPoints,

            /// <summary>Attribute, which can be improved with ability points. (Normally a charachter gets 4 of these per level.)</summary>
            WithAbilityPoints
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string ShortName { get; set; }

        [Required]
        public string Name { get; set; }

        public LevelUp Type { get; set; }
    }
}
