using System.Security.Cryptography;
using System.Text;

namespace MonitorExchange.Models
{
    public class Goods
    {
        public string Hash { get; private set; }
        public Guid Id { get; set; }
        public string strId { get; set; }
        public string Code { get; set; }
        public string Artikul { get; set; }
        public string Name { get; set; }
        public string NameUkr { get; set; }
        public string NameFull { get; set; }
        public string Type { get; set; }
        public string View { get; set; }
        public string Manufacturer { get; set; }
        public string Country { get; set; }
        public string CountryUkr { get; set; }
        public string Material { get; set; }
        public string MaterialUkr { get; set; }
        public string Season { get; set; }
        public string Color { get; set; }
        public string Categoria { get; set; }
        public string CategoriaSite { get; set; }
        public string CategoriaSiteUkr { get; set; }
        public string Sex { get; set; }
        public string Marke { get; set; }
        public string Brend { get; set; }
        public DateTime DataSeason { get; set; }
        public List<GoodsSize>? GoodsSizes { get; set; }
        public List<FEImport>? FEImports { get; set; }
        public List<FEOffers>? FEOffers { get; set; }
        
        public void UpdateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = $"{strId}{Code}{Artikul}{Name}{NameUkr}{NameFull}{Type}" +
                    $"{View}{Manufacturer}{Country}{CountryUkr}{Material}{MaterialUkr}{Season}" +
                    $"{Color}{Categoria}{CategoriaSite}{CategoriaSiteUkr}{Sex}{Marke}{Brend}{DataSeason}";  

                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                Hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); 
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Goods other)
                return Hash.Equals(other.Hash);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(strId);

    }
}
