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
   

    public class MarkCRUDService : IMarkCRUDService
    {
        private readonly ILogger _logger;

        public MarkCRUDService(ILogger logger)
        {
            _logger = logger;
        }
        private async Task<Mark> GetEntity(Guid Id)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.Marks.Where(m => m.Id == Id).FirstOrDefaultAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed GetEntity Mark {Id} : {exc}");
                throw;
            }
            
        }

        public async Task Delete(Guid Id)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var entity = await GetEntity(Id);
                    db.Entry(entity).State = EntityState.Deleted;
                    db.Marks.Remove(entity);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed delete mark {Id} : {exc}");
                throw;  
            }
            
        }

        public async Task Create(MarkDto dto)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var entity = new Mark
                    {
                        Comment = dto.Comment,
                        CreateDate = DateTime.Now,
                        Id = Guid.NewGuid(),
                        MethodOfTreatmentId = dto.MethodOfTreatmentId,
                        UserId = dto.UserId,
                        Value = dto.Value
                    };
                    db.Marks.Add(entity);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed create mark : {exc}");
            }
            
        }

        public async Task<List<MarkDto>> GetByMethod(Guid MethodId)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    return await db.Marks.Include(m => m.User).Include(m => m.MethodOfTreatment).Where(m => m.MethodOfTreatmentId == MethodId).Select(m => new MarkDto
                        {
                            Id = m.Id,
                            Comment = m.Comment,
                            CreateDate = m.CreateDate,
                            Login = m.User.Login,
                            MethodName = m.MethodOfTreatment.Title,
                            MethodOfTreatmentId = m.MethodOfTreatmentId,
                            UserId = m.UserId,
                            Value = m.Value
                        })
                        .ToListAsync();
                }
            }
            catch (Exception exc)
            {
                _logger.Error($"Failed Get List Marks By Method {MethodId} : {exc}");
                throw;
            }
          
        }

        public async Task<MarkListDto> List(int page = 0, int size = 5, string search = null)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var marks = db.Marks.Include(m=>m.MethodOfTreatment).Include(m=>m.User).AsNoTracking().AsQueryable();
                    if (!string.IsNullOrEmpty(search))
                    {
                        marks = marks.Where(mark => (mark.Comment.ToLower() == search.ToLower()));
                    }
                    var TotalCount = await marks.CountAsync();
                    marks = marks.OrderByDescending(mark => mark.CreateDate).Skip(page * size).Take(size);
                    var EntityItems = await marks.ToListAsync();
                    var items = EntityItems.Select(mark => new MarkDto
                    {
                        Id = mark.Id,
                        Comment = mark.Comment,
                        CreateDate = mark.CreateDate,
                        Login = mark.User.Login,
                        MethodName = mark.MethodOfTreatment.Title,
                        MethodOfTreatmentId = mark.MethodOfTreatmentId,
                        UserId = mark.UserId,
                        Value = mark.Value
                    }).ToList();
                    var obj = new MarkListDto
                    {
                        MarkDtos = items,
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
                _logger.Error($"Failed get list mark : {exc}");
                throw;

            }
        }
    }
}