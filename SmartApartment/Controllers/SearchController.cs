using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartApartment.Application.Contract.DTO;
using SmartApartment.Application.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartment.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchController : BaseController
    {
        ISearchService _searchService;
        public SearchController(ISearchService searchService,ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<ResponseDTO> Search(string searchText, string market)
        {
            try
            {
               return  new ResponseDTO() { Data = await _searchService.Search(searchText, market) };
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return new ResponseDTO() { ResponseStatus = ResponseStatus.Error, Message = ex.Message };
            }
        }
    }
}
