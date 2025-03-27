using MonitorExchange.Dtos.FEOffers;
using System.Xml.Serialization;

namespace MonitorExchange.Dtos.FEImport
{
    [XmlRoot("КоммерческаяИнформация", Namespace = "urn:1C.ru:commerceml_2")]
    public class AddFEImportXMLDto
    {
        [XmlElement("Каталог")]
        public CatalogImportXMLDto Catalog { get; set; }

        public void Init()
        {
            foreach (var goods in Catalog.Goodses) 
            {
                goods.strId = goods.strIdFull.Trim().Substring(0, 36);

                foreach(var req in goods.GoodsRequisites)
                {
                    if (req is null)
                        continue;

                    if (req.Name == "ВидНоменклатуры") 
                        goods.View = req.Value;

                    if (req.Name == "ТипНоменклатуры")
                        goods.Type = req.Value;

                    if (req.Name == "Полное наименование")
                        goods.NameFull = req.Value;
                }

                foreach(var pr in goods.GoodsProperties)
                {
                    if (pr is null)
                        continue;

                    if (pr.Name == "Производитель")
                        goods.Manufacturer = pr.Value;

                    if (pr.Name == "Страна")
                        goods.Country = pr.Value;

                    if (pr.Name == "СтранаУкр")
                        goods.CountryUkr = pr.Value;

                    if (pr.Name == "Материал")
                        goods.Material = pr.Value;

                    if (pr.Name == "МатериалУкр")
                        goods.MaterialUkr = pr.Value;

                    if (pr.Name == "Сезон")
                        goods.Season = pr.Value;

                    if (pr.Name == "Цвет")
                        goods.Color = pr.Value;

                    if (pr.Name == "ТоварнаяКатегория")
                        goods.Categoria = pr.Value;

                    if (pr.Name == "ТоварнаяКатегорияДляСайта")
                        goods.CategoriaSite = pr.Value;

                    if (pr.Name == "ТоварнаяКатегорияДляСайтаУкр")
                        goods.CategoriaSiteUkr = pr.Value;

                    if (pr.Name == "Пол")
                        goods.Sex = pr.Value;

                    if (pr.Name == "ТорговаяМарка")
                        goods.Marke = pr.Value;

                    if (pr.Name == "Бренд")
                        goods.Brend = pr.Value;

                    if (pr.Name == "ДатаСезона")
                        goods.DataSeason = DateTime.Parse(pr.Value);

                }

                foreach(var ch in goods.GoodsCharacteristic)
                {
                    if (ch is null)
                        continue;

                    if (ch.Name == "Размер")
                        goods.GoodsSize = new GoodsSizeXMLDto
                                            {
                                                strId = goods.strIdFull.Trim().Substring(37, 36),
                                                GoodsId = goods.strIdFull.Trim().Substring(0, 36),
                                                Name = ch.Value,
                                          };

                }
            }
        }
    }

    public class CatalogImportXMLDto
    {
        [XmlArray("Товары")]
        [XmlArrayItem("Товар")]
        public List<GoodsXMLDto> Goodses{ get; set; }
    }

    public class GoodsXMLDto
    {
        [XmlElement("Ид")]
        public string strIdFull { get; set; }

        [XmlIgnore]
        public string strId { get; set; } = string.Empty;

        [XmlElement("Код")]
        public string Code { get; set; }

        [XmlElement("Артикул")]
        public string Artikul { get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }

        [XmlElement("НаименованиеУкр")]
        public string NameUkr { get; set; }

        [XmlIgnore]
        public string NameFull { get; set; } = string.Empty;

        [XmlArray("ЗначенияРеквизитов")]
        [XmlArrayItem("ЗначениеРеквизита")]
        public List<GoodsRequisitesXMLDto> GoodsRequisites { get; set; }

        [XmlArray("ЗначенияСвойств")]
        [XmlArrayItem("ЗначенияСвойства")]
        public List<GoodsPropertiesXMLDto> GoodsProperties { get; set; }

        [XmlArray("ХарактеристикиТовара")]
        [XmlArrayItem("ХарактеристикаТовара")]
        public List<GoodsCharacteristicXMLDto> GoodsCharacteristic { get; set; }

        [XmlIgnore]
        public GoodsSizeXMLDto GoodsSize { get; set; } 

        [XmlIgnore]
        public string Type { get; set; } = string.Empty;

        [XmlIgnore]
        public string View { get; set; } = string.Empty;

        [XmlIgnore]
        public string Manufacturer { get; set; } = string.Empty;

        [XmlIgnore]
        public string Country { get; set; } = string.Empty;

        [XmlIgnore]
        public string CountryUkr { get; set; } = string.Empty;

        [XmlIgnore]
        public string Material { get; set; } = string.Empty;

        [XmlIgnore]
        public string MaterialUkr { get; set; } = string.Empty;

        [XmlIgnore]
        public string Season { get; set; } = string.Empty;

        [XmlIgnore]
        public string Color { get; set; } = string.Empty;

        [XmlIgnore]
        public string Categoria { get; set; } = string.Empty;

        [XmlIgnore]
        public string CategoriaSite { get; set; } = string.Empty;

        [XmlIgnore]
        public string CategoriaSiteUkr { get; set; } = string.Empty;

        [XmlIgnore]
        public string Sex { get; set; } = string.Empty;

        [XmlIgnore]
        public string Marke { get; set; } = string.Empty;

        [XmlIgnore]
        public string Brend { get; set; } = string.Empty;

        [XmlIgnore]
        public DateTime DataSeason { get; set; }

    }

    public class GoodsRequisitesXMLDto 
    {
        [XmlElement("Наименование")]
        public string Name{ get; set; }

        [XmlElement("Значение")]
        public string Value { get; set; }
    }

    public class GoodsPropertiesXMLDto
    {
        [XmlElement("Ид")]
        public string Name { get; set; }

        [XmlElement("Значение")]
        public string Value { get; set; }
    }

    public class GoodsCharacteristicXMLDto
    {
        [XmlElement("Наименование")]
        public string Name { get; set; }

        [XmlElement("Значение")]
        public string Value { get; set; }
    }

    public class GoodsSizeXMLDto
    {
        public string strId { get; set; }
        public string GoodsId { get; set; }
        public string Name { get; set; }
    }

}
