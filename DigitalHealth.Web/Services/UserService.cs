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
    public class UserService
    {
        private async Task<User> GetEntity(Guid id)
        {
            using (DHContext db = new DHContext())
            {
                return await db.Users.Where(u => u.Id == id).Include(u => u.Profile).Include(u => u.Role)
                    .SingleOrDefaultAsync();
            }
        }

        public async Task<UserListDto> List(int page = 0, int size = 5, string search = null)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                    var users = db.Users.AsNoTracking().AsQueryable();
                    if (!string.IsNullOrEmpty(search))
                    {
                        users = users.Where(user => (user.Login.ToLower() == search.ToLower()));
                    }
                    var TotalCount = await users.CountAsync();
                    users = users.OrderBy(user => user.Login).Skip(page * size).Take(size);
                    var EntityItems = await users.Include(d => d.Role).Include(d => d.Profile).ToListAsync();
                    var items = EntityItems.Select(user => new UserDto
                    {
                        UserId = user.Id,
                        FullName = user.Profile != null ? user.Profile.LastName + ' ' + user.Profile.FirstName + ' ' + user.Profile.MiddleName : string.Empty,
                        Gender = user.Profile != null? user.Profile.Gender : string.Empty,
                        Login = user.Login,
                        ProfileId = user.ProfileId != null ? user.ProfileId.Value : Guid.Empty,
                        RoleId = user.RoleId,
                        RoleName = user.Role.Name
                    }).ToList();
                    var obj = new UserListDto
                    {
                        UserDtos = items,
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

        public async Task SetUserRole(UserDto dto, Guid RoleId)
        {
            try
            {
                using (DHContext db = new DHContext())
                {
                   
                    var entity = await db.Users.Where(u => u.Id == dto.UserId).FirstOrDefaultAsync();
                    entity.RoleId = RoleId;
                    db.Entry(entity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                throw;
            }
           
        }

    }
}