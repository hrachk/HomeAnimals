using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeAnimals.Models
{
    public interface IApplicationDbContext
    {

        public  DbSet<Animal> Animals { get; set; }
        public  DbSet<Owner> Owners { get; set; }
        public DbSet<V_EvidenceOwner> Evidences { get; set; }
        public  void UpdateChanges();
    }
    
}