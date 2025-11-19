using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineReservation_AR.src.AirlineReservation.Application.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
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
        private static DbContextOptions<AirlineReservationDbContext>? _dbOptions;

        private static AirlineReservationDbContext? _db;
        private static PasswordHasher? _hasher;

        private static IAuthentication? _authService;
        private static AuthenticationController? _authController;

        private static IUserService? _userService;
        private static UserContrller? _userContrller;
        public static void Init()
        {
            // Load appsettings.json (giống ASP.NET Core)
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string conn = _config.GetConnectionString("AirlineReservationDatabase")
                           ?? throw new Exception("Missing connection string");

            // Build DbContextOptions
            _dbOptions = new DbContextOptionsBuilder<AirlineReservationDbContext>()
                .UseSqlServer(conn)
                .Options;

            // Khởi tạo DbContext (inject options)
            _db = new AirlineReservationDbContext(_dbOptions);

            // Utilities
            _hasher = new PasswordHasher();

            // Service layer
            _authService = new Authentication(_db, _hasher);
            _userService = new UserService(_db);

            // Controller layer
            _authController = new AuthenticationController(_authService);
            _userContrller = new UserContrller(_userService);


        }
        // expose ra ngoài cho Form gọi
        public static AuthenticationController AuthController =>
            _authController ?? throw new Exception("DI has not been initialized");

        //user
        public static UserContrller UserContrller =>
            _userContrller ?? throw new Exception("User controller is started");

    }
}
