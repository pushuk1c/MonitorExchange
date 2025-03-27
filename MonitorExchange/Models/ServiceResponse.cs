namespace MonitorExchange.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Saccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public Object? Meta { get; set; }
    }
}
