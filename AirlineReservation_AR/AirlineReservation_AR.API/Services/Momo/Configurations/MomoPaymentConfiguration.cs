namespace AirlineReservation_AR.API.Services.Momo.Configurations
{
    public class MomoPaymentConfiguration
    {
        public string partnerCode { get; set; }
        public string partnerName { get; set; }
        public string storeId { get; set; }
        public string requestId { get; set; }
        public long amount { get; set; }
        public string orderId { get; set; }
        public string orderInfo { get; set; }
        public string redirectUrl { get; set; }
        public string ipnUrl { get; set; }
        public string lang { get; set; } = "vi";
        public string extraData { get; set; } = "";
        public string requestType { get; set; } = "captureWallet";
        public string signature { get; set; }
    }
}
