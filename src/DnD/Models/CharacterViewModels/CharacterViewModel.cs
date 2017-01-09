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
                    yield return new ValidationResult($"Initial Mana + Initial Life must be equal to {Character.InitialLifePlusMana}.", new[] { "InitialLife", "InitialMana" });
                }
                else if (InitialLife < 1)
                {
                    yield return new ValidationResult($"Initial Life must be greater than 0.", new[] { "InitialLife" });
                }
                else if (InitialMana < 0)
                {
                    yield return new ValidationResult($"Initial Mana must be greater than or equal to 0.", new[] { "InitialMana" });
                }
            }
        }
    }
}
