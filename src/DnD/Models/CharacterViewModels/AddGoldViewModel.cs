using DnD.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.CharacterViewModels
{
    public class AddGoldViewModel : IValidatableObject
    {
        [Display(Name = "Adventure")]
        public int? AdventureId { get; set; }

        [Display(Name = "Character")]
        public int AdventurerId { get; set; }

        public int Amount { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>Determines, if the add gold was issued from the adventure or character view.</summary>
        /// <remarks>Possible values are e.g. Adventure or Character</remarks>
        [MaxLength(255)]
        public string From { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Amount == 0)
            {
                yield return new ValidationResult("Please select an amount", new[] { nameof(Amount) });
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
            else if (Amount + context.Gold.Where(g => g.CharacterId == AdventurerId).Sum(g => g.Value) < 0)
            {
                yield return new ValidationResult("This change would lead to a negative gold balance and is therefore not allowed", new[] { nameof(Amount) });
            }

            if(AdventureId.HasValue && !context.AdventureParticipations.Any(ap => ap.AdventurerId == AdventurerId && ap.AdventureId == AdventureId.Value))
            {
                yield return new ValidationResult("The selected character does not take part in the selected adventure", new[] { nameof(AdventureId), nameof(AdventurerId) });
            }
        }
    }
}
