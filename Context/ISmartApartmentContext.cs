using Nest;

namespace SmartApartment.Context
{
    public interface ISmartApartmentContext
    {
        public IElasticClient ElasticContext { get; set; }
    }
}
