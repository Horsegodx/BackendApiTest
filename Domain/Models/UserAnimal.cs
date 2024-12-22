using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserAnimal
    {
        public int UserAnimalId { get; set; }
        public int UserId { get; set; }
        public int AnimalsId { get; set; }

        public virtual Animal Animals { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
