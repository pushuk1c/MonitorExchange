namespace MonitorExchange.Models
{
    public class FEOffers
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public FileExchange FileExchange { get; set; }
        public Goods Goods { get; set; }
        public GoodsSize GoodsSize { get; set; }   
        public Stock Stock { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal discountedPrice { get; set; }          

    }
}
