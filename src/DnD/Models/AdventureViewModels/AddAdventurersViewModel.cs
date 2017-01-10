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
        public int AdventureId { get; set; }

        /// <summary>Character ids of adventurers which will be added.</summary>
        public IEnumerable<int> Adventurers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (!context.Adventures.Any(a => a.Id == AdventureId))
            {
                yield return new ValidationResult($"An unkown adventure was selected.", new[] { "AdventureId" });
            }

            //Linq to SQL should optimise this to a "WHERE IN" query
            if (context.Characters.Any(c => !Adventurers.Contains(c.Id)))
            {
                yield return new ValidationResult($"One or more unkown adventurers.", new[] { "Adventures" });
            }
        }
    }
}
