namespace MonitorExchange.Dtos.FileExchange
{
    public class GetFileExchangeRequestWPFDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Dictionary<string, string> Filters { get; set; } 

    }
}
