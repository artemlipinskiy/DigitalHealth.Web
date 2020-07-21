using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IMethodOfTreatmentCRUDService
    {
        Task Delete(Guid Id);
        Task Create(MethodOfTreatmentCreateDto dto);
        Task Update(MethodOfTreatmentUpdateDto dto);
        Task<MethodOfTreatmentDto> Get(Guid Id);
        Task<MethodOfTreatmentListDto> List(int page = 0, int size = 5, string search = null);
        Task<List<MethodOfTreatmentDto>> GetAll();
    }
}
