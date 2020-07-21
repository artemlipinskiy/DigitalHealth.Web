using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IUserService
    {
        Task<UserListDto> List(int page = 0, int size = 5, string search = null);
        Task SetUserRole(UserDto dto, Guid RoleId);
    }
}
