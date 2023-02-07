namespace Agenda.Application.ViewModels.Pagination
{
    public class PaginationResponse<T>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Info { get; set; }

    }
}
