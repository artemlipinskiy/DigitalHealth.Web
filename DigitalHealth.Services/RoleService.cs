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
        private readonly ILogger _logger;

        public RoleService(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<List<RoleDto>> GetAll()
        {

            try
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
            catch (Exception exc)
            {
                _logger.Error($"Failed GetAll Roles : {exc}");
                throw;
            }
        }
    }
}