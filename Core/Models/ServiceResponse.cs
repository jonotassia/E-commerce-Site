namespace Core.Models
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public T? Data { get; set; }
    }
}
