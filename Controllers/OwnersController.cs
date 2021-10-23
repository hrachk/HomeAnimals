using HomeAnimals.EntityContext.Models;
using HomeAnimals.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAnimals.Controllers
{
    [Route("animals")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _sqlDataSource;
        private readonly DataTable _dataTable;
        private SqlDataReader _sqlDataReader;
     
        private IApplicationDbContext _context;
        public OwnersController( IApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            _dataTable = new DataTable();
            _context = context;
        }


        
        [HttpPost("CreateOwner")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateOwnerRequest))]
        public ActionResult  CreateOwner(List<CreateOwnerRequest> ownerRequest)
        {           
            try
            {
                var owners =  _context.Owners;
                foreach (var item in ownerRequest)
                {
                    owners.Add(new Owner()
                    {
                        
                        OwnerId = _context.Owners.Any() ? _context.Owners.Max(q => q.OwnerId + 1)  : 1,
                        OwnerName = item.OwnerName,
                        BirthDate = item.BirthDate,
                        OwnerKind = item.OwnerKind,
                        Addres = item.Addres

                    });
                    _context.UpdateChanges();
                }
                     
              
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError,  ex  );
            }
        }

        [HttpPost("CreateAnimal")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateOwnerRequest))]
        public ActionResult CreateAnimal(List<CreateAnimalRequest> animalRequest)
        {
            try
            {
                var owners = _context.Animals;
                foreach (var item in animalRequest)
                {
                    owners.Add(new Animal()
                  {
                    AnimalId  = item.AnimalId,
                    AnimalName = item.AnimalName,
                    AnimalKind = item.AnimalKind,
                    AnimalGender = item.AnimalGender,
                    AnimalBirthDate = item.AnimalBirthDate,
                    AnimalBreed = item.AnimalBreed,
                    NumberFeedings = item.NumberFeedings,
                    LevelOfTraining = item.LevelOfTraining,
                    CatchingMouses = item.CatchingMouses,
                    OwnerId = item.OwnerId

                    });
                }
                
                _context.UpdateChanges();
                return StatusCode(201);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{animalID}")]
        public JsonResult GetAnimalsById(int animalID)
        {
            string query = string.Format("select * from Animals where id = {0}", animalID); 
            using (SqlConnection connection = new SqlConnection(_sqlDataSource))
            {
                connection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    _sqlDataReader = sqlCommand.ExecuteReader();
                    _dataTable.Load(_sqlDataReader);
                    _sqlDataReader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(_dataTable);
        }


       
        [HttpGet("Animals")]
        public async Task<IActionResult> GetAll()
        {
            List<AnimalResponse> animalsList = new List<AnimalResponse>();

            var animals = await _context.Animals.AsNoTracking().ToListAsync();
            foreach (var item in animals)
            {
                animalsList.Add(new AnimalResponse() {
                 
                    AnimalId = item.Id,
                    Kind = item.AnimalId,                     
                    AnimalName = item.AnimalName,
                    AnimalKind = item.AnimalKind,
                    AnimalGender = item.AnimalGender,
                    AnimalBirthDate = item.AnimalBirthDate,
                    AnimalBreed = item.AnimalBreed,
                    NumberFeedings = item.NumberFeedings,
                    LevelOfTraining = item.LevelOfTraining,
                    CatchingMouses = item.CatchingMouses,
                    OwnerId = item.OwnerId

                });
            } 
            return    Ok(animalsList);
        }

        [EnableCors("MyAllowSpecificOrigins")]
        [Route("OwnersList")]
        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            string query = "select * from Owners";
            var owners = await _context.Owners.FromSqlRaw(query).AsNoTracking().ToListAsync();
            if (owners == null) 
                return NotFound();
            return Ok(owners);
        }


        [Route("Summary")]
        [HttpGet]
        public JsonResult GetEvidenceOwnerList()
        {
            string query = "SELECT animals.animalID AS ID, o.ownerId, o.ownerName, o.birthDate, o.ownerKind, o.addres, animals.id AS animalID, animals.animalName, animals.animalKind, animals.animalGender, animals.animalBirthDate, animals.animalBreed, animals.numberFeedings, animals.levelOfTraining, animals.catchingMouses FROM  dbo.Owners AS o INNER JOIN dbo.Animals AS animals ON o.ownerId = animals.ownerID";
            List<V_EvidenceOwner> result = _context.Evidences.FromSqlRaw(query).AsNoTracking().ToList();
            List<EvidenceOfOwnerResponse> evidenceListResponse = new List<EvidenceOfOwnerResponse>();
            JsonResult json = MappingToList(result, evidenceListResponse); 
            return json;
        }
       
        [Route("Feeding/{animalID}")]
        [HttpPut] 
        public async Task<IActionResult> Feeding(string animalID)
        {
            //    (from p in _context.Animals
            // where p.Id == Convert.ToInt32(animalID)
            // select p).ToList() 
            //                    .ForEach(x => x.NumberFeedings+=Convert.ToInt32(count));
            //     _context.UpdateChanges();


            //return Ok("Feeded " + 1 + " time");

            #region  
            var query = from ord in _context.Animals where ord.Id == Convert.ToInt32(animalID) select ord;
            if (query == null) return null;
            foreach (Animal ord in query)
            {
                ord.NumberFeedings ++;

                
            }
            // Submit the changes to the database.
            try
            {
                _context.UpdateChanges();
                return Ok("Feeded " + 1 + " time");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            #endregion
        }

        private static JsonResult MappingToList(List<V_EvidenceOwner> result, List<EvidenceOfOwnerResponse> evidenceListResponse)
        {
            foreach (var item in result)
            {
                evidenceListResponse.Add(new EvidenceOfOwnerResponse()
                {
                    owners = new List<Owners>() {
                    new Owners() {
                        ownerId = item.OwnerId ,
                        ownerName = item.OwnerName,
                         ownerKind = item.OwnerKind,
                         addres = item.Addres,
                         birthDate = item.BirthDate,
                         animals = new List<Animals>(){ new Animals() {
                              id = item.AnimalId,
                        animalID = item.Id,
                        animalName = item.AnimalName,
                        animalKind = item.AnimalKind,
                        animalGender = item.AnimalGender,
                        animalBirthDate =item.AnimalBirthDate,
                        animalBreed = item.AnimalBreed,
                        numberFeedings = item.NumberFeedings,
                        levelOfTraining = item.LevelOfTraining,
                        catchingMouses =item.CatchingMouses
                         }
                         }
                    }
                }
                });
            }


            var groupBy = evidenceListResponse.GroupBy(item => item.owners.FirstOrDefault().ownerId).ToList()
            .Select(group => new
            {
                ownerId = group.Key,
                ownerName = group.FirstOrDefault().owners.FirstOrDefault().ownerName,
                ownerKind = group.FirstOrDefault().owners.FirstOrDefault().ownerKind,
                addres = group.FirstOrDefault().owners.FirstOrDefault().addres,
                birthDate = group.FirstOrDefault().owners.FirstOrDefault().birthDate,
                animals = group.ToList()
                .Select(animals => new
                {
                    animalID = animals.owners.FirstOrDefault().animals.FirstOrDefault().id,
                    Kind = animals.owners.FirstOrDefault().animals.FirstOrDefault().animalID,
                    animalName = animals.owners.FirstOrDefault().animals.FirstOrDefault().animalName,
                    animalKind = animals.owners.FirstOrDefault().animals.FirstOrDefault().animalKind,
                    animalGender = animals.owners.FirstOrDefault().animals.FirstOrDefault().animalGender,
                    animalBirthDate = animals.owners.FirstOrDefault().animals.FirstOrDefault().animalBirthDate,
                    animalBreed = animals.owners.FirstOrDefault().animals.FirstOrDefault().animalBreed,
                    numberFeedings = animals.owners.FirstOrDefault().animals.FirstOrDefault().numberFeedings,
                    levelOfTraining = animals.owners.FirstOrDefault().animals.FirstOrDefault().levelOfTraining,
                    catchingMouses = animals.owners.FirstOrDefault().animals.FirstOrDefault().catchingMouses,
                }).ToList()

            }).ToList();

            var json = new JsonResult(groupBy);
            return json;
        }
    }
}

 