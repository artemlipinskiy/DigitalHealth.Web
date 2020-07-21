using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.GlobalInterfaces
{
    public interface ISymptomCRUDService
    {
        Task Delete(Guid Id);
        Task Create(SymptomCreateDto dto);
        Task Update(SymptomUpdateDto dto);
        Task<SymptomUpdateDto> Get(Guid Id);
        Task<SymptomDetailsDto> GetWithDiseases(Guid Id);
        Task<SymptomListDto> List(int page = 0, int size = 5, string search = null);
        Task<List<SymptomUpdateDto>> GetAll();
    }
}
