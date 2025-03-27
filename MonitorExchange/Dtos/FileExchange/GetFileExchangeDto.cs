namespace MonitorExchange.Dtos.FileExchange
{
    public class GetFileExchangeDto
    {
        public Guid Id { get; set; }
        public DateTime DataCreate { get; set; }
        public string StrId { get; set; }
        public string Type { get; set; }
        public int Item { get; set; } = 0;
        public int InAll { get; set; } = 0;
        public bool IsUpload {  get; set; } = false;
    }
}
