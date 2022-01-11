using Nest;

namespace SmartApartment.Context
{
    public class SmartApartmentContext : ISmartApartmentContext
    {
        public  IElasticClient ElasticContext { get; set; }
        public SmartApartmentContext(IElasticClient elasticContext)
        {
            ElasticContext = elasticContext;
        }
    }
}
