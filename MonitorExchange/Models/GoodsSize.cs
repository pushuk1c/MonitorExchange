namespace MonitorExchange.Models
{
    public class GoodsSize
    {
        public Guid Id { get; set; }
        public string strId { get; set; } = string.Empty;
        public string Name { get; set; }
        public Goods Goods { get; set; }
        public List<FEImport>? FEImports { get; set; }
        public List<FEOffers>? FEOffers { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GoodsSize other)
                return strId.Equals(other.strId);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(strId);
    }
}
