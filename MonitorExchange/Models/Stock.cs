namespace MonitorExchange.Models
{
    public class Stock
    {
        public Guid Id { get; set; }
        public string StrId { get; set; }
        public string Name { get; set; }
        public List<FEOffers>? FEOffers { get; set; }

    }
}
