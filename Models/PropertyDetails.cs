namespace pruebaConecta.Models
{
    public class PropertyDetails
    {

        public string propertyTitle { get; set; }
        public DateTime publicationDate { get; set; }
        public Currency currencySale { get; set; }
        public Currency currencyRent { get; set; }
        public Currency currencySeasonalRental { get; set; }
        public string priceSaleFormatted { get; set; }
        public string priceRentFormatted { get; set; }
        public User user { get; set; }
        public Characteristics characteristics { get; set; }
        public List<PropertyMedia> propertyMedia { get; set; }
        public int idProperty { get; set; }
        public int idUser { get; set; }
        public int idClient { get; set; }
        public int idType { get; set; }
        public int idState { get; set; }
        public string webAddress { get; set; }
        public bool sale { get; set; }
        public bool rent { get; set; }
        public bool seasonalRental { get; set; }
        public string firstImage { get; set; }
        public string firstImageUrl { get; set; }
        public int priceSale { get; set; }
        public int priceRent { get; set; }
        public int priceSeasonalRental { get; set; }
        public int idCurrencySale { get; set; }
        public int idCurrencyRent { get; set; }
        public int idCurrencySeasonalRental { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public int unit { get; set; }
        public string letter { get; set; }
        public string phase { get; set; }
        public string role { get; set; }
        public int idBorough { get; set; }
        public int idSector { get; set; }
        public string coordinatesLat { get; set; }
        public string coordinatesLong { get; set; }
        public bool featured { get; set; }
        public bool onWeb { get; set; }
        public string observations { get; set; }
        public string internalObservations { get; set; }
        public string wayToVisit { get; set; }
        public bool showMapOnWeb { get; set; }
        public bool sendCoordinatesToPortals { get; set; }
        public bool markAsSale { get; set; }
        public bool markAsRent { get; set; }
        public bool visible { get; set; }

    }
}
