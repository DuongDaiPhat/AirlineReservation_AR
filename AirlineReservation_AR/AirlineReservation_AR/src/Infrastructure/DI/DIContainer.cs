using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Domain.Entities;
using AirlineReservation_AR.src.AirlineReservation.Domain.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Services;
using AirlineReservation_AR.src.AirlineReservation.Shared.Utils;
using AirlineReservation_AR.src.Application.Interfaces;
using AirlineReservation_AR.src.Application.Services;
using AirlineReservation_AR.src.Presentation__Winform_.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AirlineReservation_AR.src.Infrastructure.DI
{
    public static class DIContainer
    {
        private static IConfiguration? _config;
        public static User CurrentUser { get; private set; }
        public static DbContextOptions<AirlineReservationDbContext>? DbOptions { get; private set; }

        private static PasswordHasher? _hasher;

        private static IAuthentication? _authService;
        private static AuthenticationController? _authController;

        private static IUserService? _userService;
        private static UserContrller? _userContrller;

        private static ICityService? _cityService;
        private static CityController? _cityController;

        private static IFlightService? _flightService;
        private static FlightController? _flightController;

        private static IBookingService? _bookingService;
        private static BookingController? _bookingController;

        private static IPaymentService? _paymentService;
        private static PaymentController? _paymentController;

        public static void Init()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string conn = _config.GetConnectionString("AirlineReservationDatabase")
                           ?? throw new Exception("Missing connection string");

            // Build Options (không khởi tạo DbContext tại đây nữa)
            DbOptions = new DbContextOptionsBuilder<AirlineReservationDbContext>()
                .UseSqlServer(conn)
                .Options;

            _hasher = new PasswordHasher();

            // Service layer — mỗi hàm của service sẽ tự CreateDb()
            _authService = new Authentication(_hasher);
            _userService = new UserService();
            _cityService = new CityService();
            _flightService = new FlightService();
            _bookingService = new Application.Services.BookingService();
            _paymentService = new PaymentService();



            // Controller layer giữ nguyên
            _authController = new AuthenticationController(_authService);
            _userContrller = new UserContrller(_userService);
            _cityController = new CityController(_cityService);
            _flightController = new FlightController(_flightService);
            _bookingController = new BookingController(_bookingService);
            _paymentController = new PaymentController(_paymentService);


            _bookingService = new BookingService2(new AirlineReservationDbContext(DbOptions));
            _bookingController = new BookingController(_bookingService);
        }

        public static void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }
        public static AirlineReservationDbContext CreateDb()
        {
            if (DbOptions == null)
                throw new Exception("DI has not been initialized");

            return new AirlineReservationDbContext(DbOptions);
        }

        // expose ra ngoài cho Form gọi
        public static AuthenticationController AuthController =>
            _authController ?? throw new Exception("DI has not been initialized");

        //user
        public static UserContrller UserContrller =>
            _userContrller ?? throw new Exception("User controller is not started");
        
        //cities
        public static CityController CityController =>
            _cityController ?? throw new Exception("City controller not initialized");

        //flights
        public static FlightController FlightController => 
            _flightController ?? throw new Exception("Flight controller not initialized");
        //booking
        public static BookingController BookingController =>
            _bookingController ?? throw new Exception("Booking controller not initialized");

        //payment
        public static PaymentController paymentController =>
            _paymentController ?? throw new Exception("Payment controller not initialized");
    }
}
