namespace MonitorExchange.Models
{
    public class FEImport
    {
        public Guid Id { get; set; }
        public FileExchange? FileExchange { get; set; }
        public Goods? Goods { get; set; }
        public GoodsSize? GoodsSize { get; set; }
        public DateTime DateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is FEImport other)
                return this.FileExchange.StrId.Equals(other.FileExchange.StrId) 
                        && this.Goods.strId.Equals(other.Goods.strId) 
                        && this.GoodsSize.strId.Equals(other.GoodsSize.strId);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(this.FileExchange.StrId, this.Goods.strId, this.GoodsSize.strId);
    }
}
