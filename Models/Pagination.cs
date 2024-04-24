namespace pruebaConecta.Models
{
    public class Pagination
    {
        public Pagination()
        {
        }

        public Pagination(int totalPages, int totalRecords, int pageNumber, int pageSize)
        {
            this.totalPages = totalPages;
            this.totalRecords = totalRecords;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public int totalPages { get; set; }
        public int totalRecords { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
