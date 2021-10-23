using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAnimals.EntityContext.Models
{
    public class CreateOwnerRequest
    {
       
        [JsonProperty("ownerId")]
        public int OwnerId { get; set; }

        [JsonProperty("ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("ownerKind")]
        public string OwnerKind { get; set; }

        [JsonProperty("addres")]
        public string Addres { get; set; }
    }
}
