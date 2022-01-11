using Nest;
using SmartApartment.Repository.Shared;

namespace SmartApartment.Repository
{
    public class CustomQueryContainer
    {
        public QueryContainer QContainer { get; set; }
        public OperatorType OperatorType { get; set; }
    }
}