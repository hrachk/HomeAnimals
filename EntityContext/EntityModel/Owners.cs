
using System;

namespace HomeAnimals.Models
{
    public partial class Owner
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string OwnerKind { get; set; }
        public string Addres { get; set; }

    }
}


