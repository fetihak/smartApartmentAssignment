using AutoMapper;
using SmartApartment.Application.Contract.DTO;
using SmartApartment.Application.Contract.Interface;
using SmartApartment.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using StopWord;
using System;

namespace SmartApartment.Application.Service
{
    public class SearchService : ISearchService
    {
        private readonly IMapper _mapper;
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository, IMapper mapper)
        {
            _searchRepository = searchRepository;
            _mapper = mapper;
        }
        public async Task<List<SearchResponseDTO>> Search(string text, string market)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException();

            string trimmedSearchText = text.RemoveStopWords("en");
            if (!string.IsNullOrWhiteSpace(market)) {
                market = market.RemoveStopWords("en");
            }
            var data = await _searchRepository.Search(trimmedSearchText, market);
            
            return _mapper.Map<List<SearchResponseDTO>>(data);
        }
    }
}
