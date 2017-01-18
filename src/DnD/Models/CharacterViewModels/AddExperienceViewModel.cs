using DnD.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.CharacterViewModels
{
    public class AddExperienceViewModel : IValidatableObject
    {
        [Display(Name = "Adventure")]
        public int? AdventureId { get; set; }

        [Display(Name = "Character")]
        public int AdventurerId { get; set; }

        public int Amount { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>Determines, if the add experience was issued from the adventure or character view.</summary>
        /// <remarks>Possible values are e.g. Adventure or Character</remarks>
        [MaxLength(255)]
        public string From { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amount <= 0)
            {
                yield return new ValidationResult("Please enter a positive amount", new[] { nameof(Amount) });
            }

            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (AdventureId.HasValue && !context.Adventures.Any(a => a.Id == AdventureId.Value))
            {
                yield return new ValidationResult("An unkown adventure was selected", new[] { nameof(AdventureId) });
            }

            if (!context.Characters.Any(c => c.Id == AdventurerId))
            {
                yield return new ValidationResult("An unkown character was selected", new[] { nameof(AdventurerId) });
            }

            if (AdventureId.HasValue && !context.AdventureParticipations.Any(ap => ap.AdventurerId == AdventurerId && ap.AdventureId == AdventureId.Value))
            {
                yield return new ValidationResult("The selected character does not take part in the selected adventure", new[] { nameof(AdventureId), nameof(AdventurerId) });
            }
        }
    }
}