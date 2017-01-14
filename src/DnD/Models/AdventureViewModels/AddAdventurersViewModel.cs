using DnD.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.AdventureViewModels
{
    public class AddAdventurersViewModel : IValidatableObject
    {
        [Display(Name = "Adventure")]
        [Required]
        public int AdventureId { get; set; }

        /// <summary>Character ids of adventurers which will be added.</summary>
        [Required]
        public IEnumerable<int> Adventurers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (!context.Adventures.Any(a => a.Id == AdventureId))
            {
                yield return new ValidationResult("An unkown adventure was selected.", new[] { nameof(AdventureId) });
            }

            if (context.AdventureParticipations.Any(ap => ap.AdventureId == AdventureId && Adventurers.Contains(ap.AdventurerId)))
            {
                yield return new ValidationResult("One or more adventurers were already added to this adventure.", new[] { nameof(Adventurers) });
            }
            //Linq to SQL should optimise this to a "WHERE IN" query
            else if (context.Characters.Count(c => Adventurers.Contains(c.Id)) != Adventurers.Count())
            {
                yield return new ValidationResult("One or more unkown adventurers.", new[] { nameof(Adventurers) });
            }
        }
    }
}
