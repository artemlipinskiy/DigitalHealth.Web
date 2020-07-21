using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IRoleService
    {

        Task<List<RoleDto>> GetAll();
    }
}
