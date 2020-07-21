using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Services;
using DigitalHealth.Web.Entities;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.Web.Services
{
   
    public class SymptomCRUDService : ISymptomCRUDService
    {
        private readonly ILogger _logger;

        public SymptomCRUDService(ILogger logger)
        {
            _logger = logger;
        }
        private async Task<Symptom> GetEntity(Guid Id)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.Symptoms.SingleOrDefaultAsync(icd => icd.Id == Id);
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed GetEntity Symptom {Id} : {exc}");
                throw;
            }
        }

        public async Task Delete(Guid Id)
        {
            try
            {
                var entity = await GetEntity(Id);
                using (DHContext db = new DHContext())
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.Symptoms.Remove(entity);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed delete symptom {Id} : {exc}");
                throw;
            }
        }

        public async Task Create(SymptomCreateDto dto)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    Symptom entity = new Symptom
                    {
                        Id = Guid.NewGuid(),
                        Description = dto.Description,
                        Name = dto.Name
                    };
                    db.Symptoms.Add(entity);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed create symptom : {exc}");
                throw;
            }
        }

        public async Task Update(SymptomUpdateDto dto)
        {

            try
            {
                var entity = await GetEntity(dto.Id);
                using (DHContext db = new DHContext())
                {
                    entity.Description = dto.Description;
                    entity.Name = dto.Name;
                    db.Entry(entity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed update symptom {dto.Id} : {exc}");
                throw;
            }
        }

        public async Task<SymptomUpdateDto> Get(Guid Id)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.Symptoms.Select(Symptom => new SymptomUpdateDto
                    {
                        Id = Symptom.Id,
                        Description = Symptom.Description,
                        Name = Symptom.Name
                    }).SingleOrDefaultAsync(Symptom => Symptom.Id == Id);
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed Get SymptomDto {Id} : {exc}");
                throw;
            }
        }

        public async Task<SymptomDetailsDto> GetWithDiseases(Guid Id)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.Symptoms.Include(s =>s.Diseases).Select(Symptom => new SymptomDetailsDto
                    {
                        Id = Symptom.Id,
                        Description = Symptom.Description,
                        Name = Symptom.Name,
                        Diseases = Symptom.Diseases.Select(d =>d.Name).ToList(),
                    }).SingleOrDefaultAsync(Symptom => Symptom.Id == Id);
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed Get details Symptom {Id} : {exc}");
                throw;
            }
        }

        public async Task<SymptomListDto> List(int page = 0, int size = 5, string search = null)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var symptoms = db.Symptoms.AsNoTracking().AsQueryable();
                    if (!string.IsNullOrEmpty(search))
                    {
                        symptoms = symptoms.Where(symptom => (symptom.Name.ToLower() == search.ToLower()) ||
                                                 (symptom.Description.ToLower() == search.ToLower()));
                    }
                    var TotalCount = await symptoms.CountAsync();
                    symptoms = symptoms.OrderBy(symptom => symptom.Name).Skip(page * size).Take(size);
                    var items = await symptoms.Select(symptom => new SymptomUpdateDto()
                    {
                        Id = symptom.Id,
                        Name = symptom.Name,
                        Description = symptom.Description,
                    }).ToListAsync();

                    var obj = new SymptomListDto
                    {
                        SymptomDtos = items,
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
               _logger.Error($"Failed get list symptom : {exc}");
               throw;

            }
        }

        public async Task<List<SymptomUpdateDto>> GetAll()
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var symptoms = db.Symptoms.AsNoTracking().AsQueryable();
                    return await symptoms.Select(symptom => new SymptomUpdateDto()
                    {
                        Id = symptom.Id,
                        Name = symptom.Name,
                        Description = symptom.Description,
                    }).ToListAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed get all symptoms : {exc}");
                throw;
            }
        }
    }
}