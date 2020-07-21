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
   

    public class MethodOfTreatmentCRUDService : IMethodOfTreatmentCRUDService
    {
        private async Task<MethodOfTreatment> GetEntity(Guid Id)
        {
            using (DHContext db = new DHContext())
            {
                return await db.MethodOfTreatments.SingleOrDefaultAsync(method => method.Id == Id);
            }
        }

        public async Task Delete(Guid Id)
        {
            var entity = await GetEntity(Id);
            using (DHContext db = new DHContext())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.MethodOfTreatments.Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task Create(MethodOfTreatmentCreateDto dto)
        {
            using (DHContext db = new DHContext())
            {
                MethodOfTreatment entity = new MethodOfTreatment
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Description = dto.Description,
                    DiseaseId = dto.DiseaseId,
                    Source = dto.Source
                };
                db.MethodOfTreatments.Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(MethodOfTreatmentUpdateDto dto)
        {
            var entity = await GetEntity(dto.Id);
            using (DHContext db = new DHContext())
            {

                entity.Title = dto.Title;
                entity.Description = dto.Description;
                entity.DiseaseId = dto.DiseaseId;
                entity.Source = dto.Source;
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<MethodOfTreatmentDto> Get(Guid Id)
        {
            using (DHContext db = new DHContext())
            {
                return await db.MethodOfTreatments.Include(m=>m.Disease).Select(method => new MethodOfTreatmentDto
                {
                    Id = method.Id,
                    Description = method.Description,
                    DiseaseId = method.DiseaseId,
                    Source = method.Source,
                    DiseaseName = method.Disease.Name,
                    Title = method.Title
                    
                }).SingleOrDefaultAsync(method => method.Id == Id);
            }
        }

        public async Task<MethodOfTreatmentListDto> List(int page = 0, int size = 5, string search = null)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var methods = db.MethodOfTreatments.AsNoTracking().AsQueryable();
                    if (!string.IsNullOrEmpty(search))
                    {
                        methods = methods.Where(method => (method.Title.ToLower() == search.ToLower()) ||
                                          (method.Description.ToLower() == search.ToLower()));
                    }
                    var TotalCount = await methods.CountAsync();
                    methods = methods.OrderBy(method => method.Title).Skip(page * size).Take(size);
                    var items = await methods.Include(m=>m.Disease).Select(method => new MethodOfTreatmentDto
                    {
                        Id = method.Id,
                        Description = method.Description,
                        DiseaseId = method.DiseaseId,
                        DiseaseName = method.Disease.Name,
                        Source = method.Source,
                        Title = method.Title
                    }).ToListAsync();

                    var obj = new MethodOfTreatmentListDto
                    {
                        MethodOfTreatmentDtos = items,
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
        public async Task<List<MethodOfTreatmentDto>> GetAll()
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                  return await db.MethodOfTreatments.Include(m => m.Disease).Select(method => new MethodOfTreatmentDto
                    {
                        Id = method.Id,
                        Description = method.Description,
                        DiseaseId = method.DiseaseId,
                        DiseaseName = method.Disease.Name,
                        Source = method.Source,
                        Title = method.Title
                    }).ToListAsync();
                }
            }
            catch (Exception exc)
            {
                //Console.WriteLine(exc);

            }
            return null;
        }

    }
}