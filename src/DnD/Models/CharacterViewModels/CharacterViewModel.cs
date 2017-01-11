using DnD.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models.CharacterViewModels
{
    public class CharacterViewModel : IValidatableObject
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        [HiddenInput]
        public string Gender { get; set; }

        [Display(Name = "Race")]
        public int RaceId { get; set; }

        [Required]
        [Display(Name = "Initial Life")]
        public int InitialLife { get; set; }

        [Required]
        [Display(Name = "Initial Mana")]
        public int InitialMana { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InitialLife != 0 || InitialMana != 0)
            {
                if (InitialLife + InitialMana != Character.InitialLifePlusMana)
                {
                    yield return new ValidationResult($"Initial Mana + Initial Life has to be equal to {Character.InitialLifePlusMana}.", new[] { nameof(InitialLife), nameof(InitialMana) });
                }
                if (InitialLife < 1)
                {
                    yield return new ValidationResult($"Initial Life has to be greater than 0.", new[] { nameof(InitialLife) });
                }
                if (InitialMana < 0)
                {
                    yield return new ValidationResult($"Initial Mana has to be greater than or equal to 0.", new[] { nameof(InitialMana) });
                }
            }

            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if(!context.Races.Any(r => r.Id == RaceId))
            {
                yield return new ValidationResult($"An unkown race was selected.", new[] { nameof(RaceId) });
            }
        }
    }
}
