using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Services;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.Web.Services
{

    public class RoleService : IRoleService
    {
        public async Task<List<RoleDto>> GetAll()
        {
            using (DHContext db = new DHContext())
            {
                return await db.Roles.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name
                }).ToListAsync();
            }
        }
    }
}