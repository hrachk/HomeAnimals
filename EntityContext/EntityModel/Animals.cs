
using System;

namespace HomeAnimals.Models
{
    public class Animal
    {
        
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
        public int? OwnerId { get; set; }


    }
}


