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
        /// <summary>Id of AdventureParticipation for which gold will be added / removed.</summary>
        public int Id { get; set; }

        public int Amount { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if(!context.AdventureParticipations.Any(ap => ap.Id == Id))
            {
                yield return new ValidationResult("An unkown adventurer / adventure combination was selected", new[] { nameof(Id) });
            }

            if(Amount + context.Gold.Where(gold => gold.LootFromId == Id).Sum(gold => gold.Value) < 0)
            {
                yield return new ValidationResult("This change would lead to a negative gold balance and is therefor not allowed.", new[] { nameof(Amount) });
            }
        }
    }
}
