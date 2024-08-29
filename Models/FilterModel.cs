namespace Shopdemo1.Models
{
    public class FilterModel
    {
        public string SearchString { get; set; }
        public string Actor { get; set; }
        public DateTime? Date { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Type { get; set; }
    }
}
