﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class Character
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string OwnerId { get; set; }
        [Required]
        public ApplicationUser Owner { get; set; }

        public int RaceId { get; set; }
        [Required]
        public Race Race { get; set; }
    }
}
