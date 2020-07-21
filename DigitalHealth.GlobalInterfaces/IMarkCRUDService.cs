using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IMarkCRUDService
    {
        Task Delete(Guid Id);
        Task Create(MarkDto dto);
        Task<List<MarkDto>> GetByMethod(Guid MethodId);
        Task<MarkListDto> List(int page = 0, int size = 5, string search = null);
    }
}
