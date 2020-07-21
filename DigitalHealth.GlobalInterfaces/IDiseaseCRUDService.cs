using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface IDiseaseCRUDService
    {
        Task Delete(Guid Id);
        Task Create(DiseaseCreateDto dto);
        Task Update(DiseaseUpdateDto dto);
        Task<DiseaseUpdateDto> Get(Guid Id);
        Task<DiseaseListDto> List(int page = 0, int size = 5, string search = null);
        Task<List<DiseaseDto>> GetAll();
    }
}
