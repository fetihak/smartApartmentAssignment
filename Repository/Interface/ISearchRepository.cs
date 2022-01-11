using SmartApartment.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartApartment.Repository.Interface
{
    public interface ISearchRepository
    {
        Task<List<SearchResponse>> Search(string text, string market);
    }
}
