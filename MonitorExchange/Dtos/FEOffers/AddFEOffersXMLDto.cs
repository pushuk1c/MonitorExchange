using System.Xml.Serialization;

namespace MonitorExchange.Dtos.FEOffers
{
    [XmlRoot("КоммерческаяИнформация", Namespace = "urn:1C.ru:commerceml_2")]
    public class AddFEOffersXMLDto
    {
        [XmlElement("ПакетПредложений")]
        public PacketOffersXMLDto Packet { get; set; }        
    }

    public class PacketOffersXMLDto
    {
        [XmlArray("Склады")]
        [XmlArrayItem("Склад")]
        public List<StockXMLDto> Stocks { get; set; }

        [XmlArray("Предложения")]
        [XmlArrayItem("Предложение")]
        public List<OfferXMLDto> Offers { get; set; }  
    }
     
    public class StockXMLDto
    {
        [XmlElement("Ид")]
        public string StrId {  get; set; }

        [XmlElement("Наименование")]
        public string Name { get; set; }

    }

    public class OfferXMLDto
    {
        [XmlElement("Ид")]
        public string strId { get; set; }

        [XmlArray("Цены")]
        [XmlArrayItem("Цена")]
        public List<PriceXMLDto> Prices { get; set; }

        [XmlElement("Склад")]
        public List<OfferStockXMLDto> OfferStocks { get; set; }

    }

    public class PriceXMLDto
    {
        [XmlElement("ЦенаЗаЕдиницу")]
        public decimal standartPrice { get; set; }

        [XmlElement("ЦенаЗаЕдиницуСоСкидкой")]
        public decimal discountedPrice { get; set; }
    }

    public class OfferStockXMLDto
    {
        [XmlAttribute("ИдСклада")]
        public string strIdStock { get; set; }

        [XmlAttribute("КоличествоНаСкладе")]
        public int Quantity { get; set; }
    }


}
