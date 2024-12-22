using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            News = new HashSet<News>();
            Payments = new HashSet<Payment>();
            PollAnswers = new HashSet<PollAnswer>();
            SntEvents = new HashSet<SntEvent>();
            Snts = new HashSet<Snt>();
            UserAnimals = new HashSet<UserAnimal>();
            UserPlants = new HashSet<UserPlant>();
            UserShops = new HashSet<UserShop>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
        public virtual ICollection<SntEvent> SntEvents { get; set; }
        public virtual ICollection<Snt> Snts { get; set; }
        public virtual ICollection<UserAnimal> UserAnimals { get; set; }
        public virtual ICollection<UserPlant> UserPlants { get; set; }
        public virtual ICollection<UserShop> UserShops { get; set; }
    }
}
