namespace MonitorExchange.Dtos.FileExchange
{
    public class AddFileExchangeDto
    {
        public DateTime DataCreate { get; set; }
        public string StrId { get; set; }
        public string Type { get; set; }
        public int Item { get; set; } = 0;
        public int InAll { get; set; } = 0;
    }
}
