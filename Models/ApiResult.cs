namespace pruebaConecta.Models
{
    public class ApiResult
    {
        public List<Property> items { get; set; }
        //public PropertyDetails propertyDetails { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public Pagination pagination { get; set; }
    }
}
