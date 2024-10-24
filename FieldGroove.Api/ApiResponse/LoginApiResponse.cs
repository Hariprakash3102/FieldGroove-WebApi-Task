namespace FieldGroove.Api.ApiResponse
{
    public class LoginApiResponse<T>
    {
        public T Data { get; set; }
        public string Token { get; set; }
        public string Status {  get; set; }
        public DateTime Timestamp { get; set; }
    }
}
