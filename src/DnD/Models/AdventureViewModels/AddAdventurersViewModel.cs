using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.AdventureViewModels
{
    public class AddAdventurersViewModel
    {
        [Display(Name = "Adventure")]
        public int AdventureId { get; set; }

        /// <summary>Character ids of adventurers which will be added.</summary>
        public IEnumerable<int> Adventurers { get; set; }
    }
}
