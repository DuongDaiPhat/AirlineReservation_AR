namespace AirlineReservation_AR.API.Interfaces
{
    public interface IPaymentCallbackService
    {
        string UpdatePaymentStatus(string orderId, int resultCode, string message);
    }
}
