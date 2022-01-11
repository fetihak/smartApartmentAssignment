using SmartApartment.Application.Contract.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartApartment.Application.Contract.Interface
{
    public interface ISearchService
    {
        Task<List<SearchResponseDTO>> Search(string text, string market);
    }
}
