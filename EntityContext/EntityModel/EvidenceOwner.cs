
using System;

namespace HomeAnimals.Models
{
    public class V_EvidenceOwner
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string OwnerKind { get; set; }
        public string Addres { get; set; }
        public int Id { get; set; }
        public int? AnimalId { get; set; }
        public string AnimalName { get; set; }
        public string AnimalKind { get; set; }
        public string AnimalGender { get; set; }
        public DateTime? AnimalBirthDate { get; set; }
        public string AnimalBreed { get; set; }
        public int? NumberFeedings { get; set; }
        public int? LevelOfTraining { get; set; }
        public bool? CatchingMouses { get; set; }

    }
}


