using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class RaceAttribute
    {
        public int Id { get; set; }

        public int RaceId { get; set; }
        [Required]
        public Race Race { get; set; }

        public int AttributeId { get; set; }
        [Required]
        public DnDAttribute Attribute { get; set; }

        /// <summary>
        /// Default value, that this race has in the given attribute.
        /// </summary>
        public int Value { get; set; }
    }
}
