namespace SmartApartment.Domain.Entity
{
    public enum ObjectType
    {
        Property,
        Management
    }

    public class SearchResponse
    {
        public long id { get; set; }
        public ObjectType type { get; set; }
        public string name { get; set; }
        public string market { get; set; }
        public string state { get; set; }
    }
}
