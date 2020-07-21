using DigitalHealth.Web.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IICDCRUDService
    {
        Task Delete(Guid Id);
        Task Create(ICDDto dto);
        Task Update(ICDDto dto);
        Task<ICDDto> Get(Guid Id);
        Task<ICDListDto> List(int page = 0, int size = 5, string search = null);
        Task<List<ICDDto>> GetAll();
        Task<int> GetTotalCount();
    }
}
