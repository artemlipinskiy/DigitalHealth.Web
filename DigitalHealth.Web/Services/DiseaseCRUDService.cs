using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitalHealth.Web.Entities;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.Web.Services
{
    public class DiseaseCRUDService
    {
        private async Task<Disease> GetEntity(Guid Id)
        {
            using (DHContext db = new DHContext())
            {
                return await db.Diseases.SingleOrDefaultAsync(Disease => Disease.Id == Id);
            }
        }

        public async Task Delete(Guid Id)
        {
            var entity = await GetEntity(Id);
            using (DHContext db = new DHContext())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.Diseases.Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task Create(DiseaseCreateDto dto)
        {
            using (DHContext db = new DHContext())
            {
                Disease entity = new Disease
                {
                    Id = Guid.NewGuid(),
                    Description = dto.Description,
                    Name = dto.Name,
                    ICDID = dto.ICDID
                };
                if (dto.SymptomIds != null)
                    foreach (var item in dto.SymptomIds)
                    {
                        entity.Symptoms.Add(await db.Symptoms.FirstOrDefaultAsync(s => s.Id == item));
                    }
                db.Diseases.Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(DiseaseUpdateDto dto)
        {
            try
            {
               
                using (DHContext db = new DHContext())
                {
                    var entity = await db.Diseases.Include(d =>d.Symptoms).SingleOrDefaultAsync(Disease => Disease.Id == dto.Id);
                    entity.Description = dto.Description;
                    entity.Name = dto.Name;
                    entity.ICDID = dto.ICDID;
                    entity.Symptoms.Clear();
                    if (dto.SymptomIds != null)
                        foreach (var item in dto.SymptomIds)
                        {
                            var symptomdb = await db.Symptoms.FirstOrDefaultAsync(s => s.Id == item);
                            entity.Symptoms.Add(symptomdb);
                        }
                    // TODO UPDATE SYMPTOMS
                    db.Entry(entity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        public async Task<DiseaseUpdateDto> Get(Guid Id)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.Diseases.Include(d => d.Symptoms).Select(Disease => new DiseaseUpdateDto
                    {
                        Id = Disease.Id,
                        Description = Disease.Description,
                        Name = Disease.Name,
                        ICDID = Disease.ICDID,
                        SymptomIds =  Disease.Symptoms.Select(s => s.Id).ToList(),
                        SymptomNames = Disease.Symptoms.Select(s => s.Name).ToList()
                    }).SingleOrDefaultAsync(Disease => Disease.Id == Id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
           
        }

        public async Task<DiseaseListDto> List(int page = 0, int size = 5, string search = null)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var diseases = db.Diseases.AsNoTracking().AsQueryable();
                    if (!string.IsNullOrEmpty(search))
                    {
                        diseases = diseases.Where(disease => (disease.Name.ToLower() == search.ToLower()) ||
                                                 (disease.Description.ToLower() == search.ToLower()));
                    }
                    var TotalCount = await diseases.CountAsync();
                    diseases = diseases.OrderBy(disease => disease.Name).Skip(page * size).Take(size);
                    var EntityItems = await diseases.Include(d=>d.ICD).Include(d=>d.Symptoms).ToListAsync();
                    var items = EntityItems.Select(disease => new DiseaseDto
                    {
                        Id = disease.Id,
                        Description = disease.Description,
                        ICDID = disease.ICDID,
                        ICDName = disease.ICDID!=null ? disease.ICD.Code : string.Empty,
                        Name = disease.Name,
                        SymptomIds = disease.Symptoms!=null ? disease.Symptoms.Select(s => s.Id).ToList() : new List<Guid>(),
                        SymptomNames = disease.Symptoms != null ? disease.Symptoms.Select(s=>s.Name).ToList() : new List<string>()
                    }).ToList();
                    var obj = new DiseaseListDto
                    {
                        DiseaseDtos = items,
                        TotalCount = TotalCount,
                        Page = page + 1,
                        PageSize = size,
                        PageCount = (TotalCount % size) > 0 ? TotalCount / size + 1 : TotalCount / size
                    };
                    return obj;
                }
            }
            catch (Exception exc)
            {
                //Console.WriteLine(exc);

            }
            return null;
        }

        public async Task<List<DiseaseDto>> GetAll()
        {
            using (DHContext db = new DHContext())
            {
                var dbItems = await db.Diseases.Include(d => d.ICD).Include(d => d.Symptoms).ToListAsync();
                return dbItems.Select(disease => new DiseaseDto
                {
                    Id = disease.Id,
                    Description = disease.Description,
                    ICDID = disease.ICDID,
                    ICDName = disease.ICDID != null ? disease.ICD.Code : string.Empty,
                    Name = disease.Name,
                    SymptomIds = disease.Symptoms != null
                        ? disease.Symptoms.Select(s => s.Id).ToList()
                        : new List<Guid>(),
                    SymptomNames = disease.Symptoms != null
                        ? disease.Symptoms.Select(s => s.Name).ToList()
                        : new List<string>()
                }).ToList();
            }
        }
    }
}