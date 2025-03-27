using System.Xml;

namespace MonitorExchange.Models
{
    public class FileExchange
    {
        public Guid Id { get; set; }
        public DateTime DataCreate { get; set; }
        public string StrId { get; set; }
        public string Type { get; set; }
        public int Item { get; set; } = 0;
        public int InAll { get; set; } = 0;
        public bool IsUpload { get; set; }
        public List<FEImport> FEImports { get; set; }
        public List<FEOffers> FEOffers { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is FileExchange other)
                return StrId.Equals(other.StrId);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(StrId);
    }
    
}
