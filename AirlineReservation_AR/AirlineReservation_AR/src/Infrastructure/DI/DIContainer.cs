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
        public static User? CurrentUser { get; private set; }
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

        private static IBookingServiceAdmin? _bookingServiceAdmin;
        private static BookingControllerAdmin? _bookingControllerAdmin;

        private static IFlightServiceAdmin? _flightServiceAdmin;
        private static FlightControllerAdmin? _flightControllerAdmin;

        private static IFlightPricingServiceAdmin? _flightPricingServiceAdmin;
        private static IPromotionServiceAdmin? _promotionServiceAdmin;
        private static IUnitOfWorkAdmin? _unitOfWorkAdmin;
        private static IReportServiceAdmin? _reportServiceAdmin;

        private static PricingControllerAdmin? _pricingControllerAdmin;
        private static PromotionControllerAdmin? _promotionControllerAdmin;
        private static ReportControllerAdmin? _reportController;

        private static IPromotionService? _promotionService;
        private static PromotionController? _promotionController;

        private static IFareRuleService? _fareRuleService;
        private static FareRuleController? _fareRuleController;

        private static IRescheduleService? _rescheduleService;
        private static RescheduleController? _rescheduleController;

        private static TicketDetailController? _ticketDetailController;

        private static ISeatClassService? _seatClassService;
        private static SeatClassController? _seatClassController;

        private static IStaffDashboardService? _staffDashboardService;
        private static StaffDashboardController? _staffDashboardController;

        private static ILookupService? _lookupService;


        public static void Init()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string conn = _config.GetConnectionString("AirlineReservationDatabase")
               ?? throw new Exception("Missing connection string");

            DbOptions = new DbContextOptionsBuilder<AirlineReservationDbContext>()
                .UseSqlServer(
                    conn,
                    opt => opt.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null
                    )
                )
                .Options;
            _hasher = new PasswordHasher();

            var pricingRepo = new FlightPricingRepositoryAdmin(new AirlineReservationDbContext(DbOptions));
            var promotionRepo = new PromotionRepositoryAdmin(new AirlineReservationDbContext(DbOptions));

            _unitOfWorkAdmin = new UnitOfWorkAdmin(
                new AirlineReservationDbContext(DbOptions),
                pricingRepo,
                promotionRepo
            );

            // Service layer — mỗi hàm của service sẽ tự CreateDb()
            _authService = new Authentication(_hasher);
            _userService = new UserService();
            _cityService = new CityService();
            _flightService = new FlightService();
            _bookingService = new Application.Services.BookingServices();
            _paymentService = new PaymentService();
            _bookingServiceAdmin = new BookingServiceAdmin();
            _flightServiceAdmin = new FlightServiceAdmin();
            _flightPricingServiceAdmin = new FlightPricingServiceAdmin(_unitOfWorkAdmin);
            _promotionServiceAdmin = new PromotionServiceAdmin(_unitOfWorkAdmin);
            _reportServiceAdmin = new ReportServiceAdmin(new AirlineReservationDbContext(DbOptions));
            _bookingService = new Application.Services.BookingServices();
            _paymentService = new PaymentService();
            _promotionService = new PromotionService();
            _fareRuleService = new FareRuleService();
            _rescheduleService = new RescheduleService();
        
            _staffDashboardService = new StaffDashboardService();

            // Lookup service for dropdowns
            _lookupService = new LookupService();


            // Controller layer giữ nguyên
            _authController = new AuthenticationController(_authService);
            _userContrller = new UserContrller(_userService);
            _cityController = new CityController(_cityService);
            _flightController = new FlightController(_flightService);

        
            _bookingController = new BookingController(_bookingService);
            _paymentController = new PaymentController(_paymentService);
            _bookingControllerAdmin = new BookingControllerAdmin(_bookingServiceAdmin);
            _flightControllerAdmin = new FlightControllerAdmin(_flightServiceAdmin);
            _pricingControllerAdmin = new PricingControllerAdmin(_flightPricingServiceAdmin);
            _promotionControllerAdmin = new PromotionControllerAdmin(_promotionServiceAdmin);
            _reportController = new ReportControllerAdmin(_reportServiceAdmin);

            //_bookingService = new BookingService2(new AirlineReservationDbContext(DbOptions));
            _bookingController = new BookingController(_bookingService);
            _paymentController = new PaymentController(_paymentService);


            //_bookingService = new BookingService2(new AirlineReservationDbContext(DbOptions));
            _bookingController = new BookingController(_bookingService);
            _promotionController = new PromotionController(_promotionService);
            _fareRuleController = new FareRuleController(_fareRuleService);
            _rescheduleController = new RescheduleController(_rescheduleService);
            _ticketDetailController = new TicketDetailController(
                _rescheduleService,
                _fareRuleService
            );
            _seatClassService = new SeatClassService();
            _seatClassController = new SeatClassController(_seatClassService);
            _staffDashboardController = new StaffDashboardController(_staffDashboardService);
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


        //public static IBookingService BookingService =>
        //    _bookingService ?? throw new Exception("BookingService not initialized");

        //payment
        public static PaymentController paymentController =>
            _paymentController ?? throw new Exception("Payment controller not initialized");

        //booking admin
        public static BookingControllerAdmin BookingControllerAdmin =>
            _bookingControllerAdmin ?? throw new Exception("Booking Admin controller not initialized");

        //flight admin
        public static FlightControllerAdmin FlightControllerAdmin =>
            _flightControllerAdmin ?? throw new Exception("Flight Admin controller not initialized");

        //pricing admin
        public static PricingControllerAdmin PricingControllerAdmin =>
            _pricingControllerAdmin ?? throw new Exception("Pricing Admin controller not initialized");

        //promotion admin
        public static PromotionControllerAdmin PromotionControllerAdmin =>
            _promotionControllerAdmin ?? throw new Exception("Promotion Admin controller not initialized");

        //report admin
        public static ReportControllerAdmin ReportControllerAdmin =>
            _reportController ?? throw new Exception("Report Admin controller not initialized");

        //promotion
        public static PromotionController PromotionController =>
        _promotionController ?? throw new Exception("Promotion controller not initialized");

        // staff dashboard
        public static StaffDashboardController StaffDashboardController =>
        _staffDashboardController ?? throw new Exception("StaffDashboard controller not initialized");

        //seatClass
        public static SeatClassController SeatClassController =>
        _seatClassController ?? throw new Exception("SeatClass controller not initialized");

        public static RescheduleController RescheduleConroller =>
        _rescheduleController ?? throw new Exception("Reschedule controller not initialized");

        public static TicketDetailController TicketDetailController =>
        _ticketDetailController ?? throw new Exception("Ticket Detail controller not initialized");

        // Lookup service for dropdowns
        public static ILookupService LookupService =>
            _lookupService ?? throw new Exception("Lookup service not initialized");
    }
}
