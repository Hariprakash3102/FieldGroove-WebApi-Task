namespace FieldGroove.Api.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }
    }
}
