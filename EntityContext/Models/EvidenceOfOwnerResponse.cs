using System;
using System.Collections.Generic;

namespace HomeAnimals.Models
{
    /// <summary>
    /// Owner with list of his animals
    /// </summary>
    //  public class EvidenceOfOwnerResponse
    public class EvidenceOfOwnerResponse
    {
        public List<Owners> owners { get; set; }
      
    }
    public class Owners
    {
        public int ownerId { get; set; }
        public string ownerName { get; set; }
        public DateTime? birthDate { get; set; }
        public string ownerKind { get; set; }
        public string addres { get; set; }
        public List<Animals> animals { get; set; }
    }
    public class Animals
    {
        public int? id { get; set; }
        public int? animalID { get; set; }
        public string animalName { get; set; }
        public string animalKind { get; set; }
        public string animalGender { get; set; }
        public DateTime? animalBirthDate { get; set; }
        public string animalBreed { get; set; }
        public int? numberFeedings { get; set; }
        public int? levelOfTraining { get; set; }
        public bool? catchingMouses { get; set; }
    }
  
}
