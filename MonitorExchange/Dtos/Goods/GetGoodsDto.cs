namespace MonitorExchange.Dtos.Goods
{
    public class GetGoodsDto
    {
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
    }
}
