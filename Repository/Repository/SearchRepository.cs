using System;
using Nest;
using SmartApartment.Context;
using SmartApartment.Domain.Entity;
using SmartApartment.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SmartApartment.Repository.Shared;

namespace SmartApartment.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ISmartApartmentContext _context;
        public IConfiguration Configuration { get; }

        public SearchRepository(ISmartApartmentContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<List<SearchResponse>> Search(string text, string market)
        {
            List<SearchResponse> responseList = new List<SearchResponse>();          
            List<CustomQueryContainer> queryContainers = new List<CustomQueryContainer>();

            if (!string.IsNullOrWhiteSpace(market))
            {
                queryContainers.Add(MapQuery("property.name", text, OperatorType.AND));
                queryContainers.Add(MapQuery("property.formerName", text));
                queryContainers.Add(MapQuery("property.market", market, OperatorType.AND));
            }

            queryContainers.Add(MapQuery("mgmt.name", text));
            queryContainers.Add(MapQuery("mgmt.market", market));


            var searchRequest = new SearchRequest(Configuration.GetSection("Elastic:Index").Value)
            {
                From = 0,
                Size = 50,
                Query = BuildQuery(queryContainers)
            };

            var searchResponse = await _context.ElasticContext.SearchAsync<SearchResult>(searchRequest);

            if (searchResponse.IsValid)
            {
                var result = searchResponse.Documents.ToList();
                responseList = MapResponse(result);
            }

            return responseList;
        }

        private CustomQueryContainer MapQuery(string field, string value, OperatorType operatorType = OperatorType.OR)
        {
            return new CustomQueryContainer()
            {
                QContainer = new MatchQuery()
                {
                    Field = field,
                    Query = value
                },
                OperatorType = operatorType
            };
        }

        private QueryContainer BuildQuery(List<CustomQueryContainer> queryContainers)
        {
            QueryContainer queries = new QueryContainer();

            foreach (CustomQueryContainer q in queryContainers)
            {
                switch (q.OperatorType)
                {
                    case OperatorType.AND:
                        queries = queries & q.QContainer;
                        break;

                    case OperatorType.OR:
                    default:
                        queries = queries | q.QContainer;
                        break;
                }
                
            }

            return queries;
        }

        private List<SearchResponse> MapResponse(List<SearchResult> result)
        {
            List<SearchResponse> responseList = new List<SearchResponse>();

            foreach (var doc in result)
            {
                if (doc.mgmt != null)
                {
                    var response = new SearchResponse();
                    response.type = ObjectType.Management;
                    response.id = doc.mgmt.mgmtID;
                    response.name = doc.mgmt.name;
                    response.market = doc.mgmt.market;
                    response.state = doc.mgmt.state;
                    responseList.Add(response);
                }

                if (doc.property != null)
                {
                    var response = new SearchResponse();
                    response.type = ObjectType.Property;
                    response.id = doc.property.propertyID;
                    response.name = doc.property.name;
                    response.market = doc.property.market;
                    response.state = doc.property.state;
                    responseList.Add(response);
                }
            }
     
            return responseList;
        }
    }
}
