using SmartApartment.Application.Contract.Enum;

namespace SmartApartment.Application.Contract.DTO
{
    public class SearchResponseDTO
    {
        public long id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string market { get; set; }
        public string state { get; set; }
    }
}
