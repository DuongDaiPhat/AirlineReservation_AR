namespace AirlineReservation_AR.API.Interfaces
{
    public interface IEmailService
    {
        Task SendBookingQrAsync(int bookingId);
        Task RequestCodeAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task ResetPasswordAsync(string email, string newPassword);
    }
}
