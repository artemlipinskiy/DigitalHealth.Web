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

    public class ICDCRUDService : IICDCRUDService
    {
        private async Task<ICD> GetEntity(Guid Id)
        {
            using (DHContext db = new DHContext())
            {
                return await db.ICDs.SingleOrDefaultAsync(icd => icd.Id == Id);
            }
        }

        public async Task Delete(Guid Id)
        {
            var entity = await GetEntity(Id);
            using (DHContext db = new DHContext())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.ICDs.Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task Create(ICDDto dto)
        {
            using (DHContext db = new DHContext())
            {
                ICD entity = new ICD
                {
                    Id = Guid.NewGuid(),
                    Code = dto.Code,
                    Description = dto.Description,
                    Name = dto.Name
                };
                db.ICDs.Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(ICDDto dto)
        {
            var entity = await GetEntity(dto.Id);
            using (DHContext db = new DHContext())
            {

                entity.Code = dto.Code;
                entity.Description = dto.Description;
                entity.Name = dto.Name;
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }

        public async Task<ICDDto> Get(Guid Id)
        {
            using (DHContext db = new DHContext())
            {
                return await db.ICDs.Select(icd =>new ICDDto
                {
                    Id = icd.Id,
                    Code = icd.Code,
                    Description = icd.Description,
                    Name = icd.Name
                }).SingleOrDefaultAsync(icd => icd.Id == Id);
            }
        }

        public async Task<ICDListDto> List(int page = 0, int size = 5, string search = null)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var icds = db.ICDs.AsNoTracking().AsQueryable();
                    if (!string.IsNullOrEmpty(search))
                    {
                        icds = icds.Where(icd => (icd.Code.ToLower() == search.ToLower()) ||
                                          (icd.Name.ToLower()== search.ToLower()));
                    }
                    var TotalCount = await icds.CountAsync();
                    icds = icds.OrderBy(icd => icd.Name).Skip(page * size).Take(size);
                    var items = await icds.Select(icd => new ICDDto
                    {
                        Id = icd.Id,
                        Name = icd.Name,
                        Description = icd.Description,
                        Code = icd.Code
                    }).ToListAsync();
                   
                    var obj = new ICDListDto{
                        IcdDtos = items,
                        TotalCount = TotalCount,
                        Page = page+1,
                        PageSize = size,
                        PageCount = (TotalCount % size)>0? TotalCount / size+1 : TotalCount / size 
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
        public async Task<List<ICDDto>> GetAll()
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.ICDs.Select(icd => new ICDDto
                    {
                        Id = icd.Id,
                        Name = icd.Name,
                        Description = icd.Description,
                        Code = icd.Code
                    }).ToListAsync();
                }
            }
            catch (Exception exc)
            {
                //Console.WriteLine(exc);

            }
            return null;
        }

        public async Task<int> GetTotalCount()
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.ICDs.CountAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
           
        }
    }
}