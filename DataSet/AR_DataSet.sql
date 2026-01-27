-- =============================================
-- AIRLINE RESERVATION SYSTEM - FINAL DATA (UPDATED SCHEMA)
-- Date Range: 20/11/2025 - 20/12/2025
-- Note: Price, Taxes, Fees moved from Tickets to Bookings
-- =============================================

USE AirlineReservationSystem;
GO

PRINT '';
PRINT '=============================================';
PRINT '   STARTING DATA IMPORT - UPDATED SCHEMA';
PRINT '=============================================';
PRINT '';

-- =============================================
-- STEP 0: VERIFY ROLES & SEATCLASSES EXIST
-- =============================================

PRINT 'STEP 0: Verifying Roles & SeatClasses...';

DECLARE @RoleCount INT;
SELECT @RoleCount = COUNT(*) FROM Roles;
PRINT '  Roles: ' + CAST(@RoleCount AS NVARCHAR(10));

DECLARE @SeatClassCount INT;
SELECT @SeatClassCount = COUNT(*) FROM SeatClasses;
PRINT '  SeatClasses: ' + CAST(@SeatClassCount AS NVARCHAR(10));

-- =============================================
-- STEP 1: SETUP HELPER TABLES
-- =============================================

PRINT '';
PRINT 'STEP 1: Creating helper tables...';

IF OBJECT_ID('tempdb..#VietnamNames') IS NOT NULL DROP TABLE #VietnamNames;
IF OBJECT_ID('tempdb..#InternationalNames') IS NOT NULL DROP TABLE #InternationalNames;
IF OBJECT_ID('tempdb..#LastNames') IS NOT NULL DROP TABLE #LastNames;

CREATE TABLE #VietnamNames (FirstName NVARCHAR(50));
INSERT INTO #VietnamNames VALUES
('Nguyên'), ('Hùng'), ('Minh'), ('Tuấn'), ('Khoa'),
('Hương'), ('Linh'), ('Phương'), ('Thanh'), ('Trang'),
('Hằng'), ('Dũng'), ('Kiên'), ('Long'), ('Hiệp'),
('Tâm'), ('Quyên'), ('Vân'), ('Oanh'), ('Ý');

CREATE TABLE #InternationalNames (FirstName NVARCHAR(50));
INSERT INTO #InternationalNames VALUES
('John'), ('Emma'), ('Liam'), ('Olivia'), ('Noah'),
('Ava'), ('Ethan'), ('Sophia'), ('Mason'), ('Isabella');

CREATE TABLE #LastNames (LastName NVARCHAR(50));
INSERT INTO #LastNames VALUES
('Smith'), ('Johnson'), ('Williams'), ('Jones'), ('Brown'),
('Nguyễn'), ('Trần'), ('Phạm'), ('Lê'), ('Đặng');

PRINT '  ✓ Helper tables created';

-- =============================================
-- STEP 2: INSERT PERMISSIONS & ROLEPERMISSIONS
-- =============================================

PRINT '';
PRINT 'STEP 2: Inserting Permissions & RolePermissions...';

DELETE FROM RolePermissions;
DELETE FROM Permissions;

INSERT INTO Permissions (PermissionName, Description, Module) VALUES
('BookFlight', 'Book a flight', 'booking'),
('ViewBooking', 'View booking details', 'booking'),
('CancelBooking', 'Cancel booking', 'booking'),
('ModifyBooking', 'Modify booking', 'booking'),
('ProcessPayment', 'Process payment', 'payment'),
('ViewUsers', 'View user list', 'user_management'),
('CreateUser', 'Create new user', 'user_management'),
('DeleteUser', 'Delete user', 'user_management'),
('ManageFlights', 'Manage flights', 'flight_management'),
('ManageAirlines', 'Manage airlines', 'flight_management'),
('ViewAuditLog', 'View audit logs', 'audit'),
('ManagePromotions', 'Manage promotions', 'promotion');

-- Assign Permissions to Roles
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 1, PermissionID FROM Permissions;

INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 2, PermissionID FROM Permissions 
WHERE PermissionName IN ('BookFlight', 'ViewBooking', 'ProcessPayment', 'ViewUsers', 'ManageFlights');

INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 3, PermissionID FROM Permissions 
WHERE PermissionName IN ('BookFlight', 'ViewBooking', 'CancelBooking', 'ProcessPayment');

DECLARE @PermissionCount INT;
SELECT @PermissionCount = COUNT(*) FROM Permissions;
PRINT '  ✓ Permissions: ' + CAST(@PermissionCount AS NVARCHAR(10));

-- =============================================
-- STEP 3: INSERT COUNTRIES & CITIES
-- =============================================

PRINT '';
PRINT 'STEP 3: Inserting Countries & Cities...';

DELETE FROM Cities WHERE CountryCode IN ('VNM', 'THA', 'SGP', 'MYS', 'IDN', 'PHL', 'JPN', 'KHM', 'LAO', 'MMR', 'USA', 'GBR', 'FRA', 'DEU', 'AUS', 'CHN', 'KOR', 'IND');
DELETE FROM Countries WHERE CountryCode IN ('VNM', 'THA', 'SGP', 'MYS', 'IDN', 'PHL', 'JPN', 'KHM', 'LAO', 'MMR', 'USA', 'GBR', 'FRA', 'DEU', 'AUS', 'CHN', 'KOR', 'IND');

INSERT INTO Countries (CountryCode, CountryName, Currency, IsActive) VALUES
('VNM', 'Vietnam', 'VND', 1), ('THA', 'Thailand', 'THB', 1), ('SGP', 'Singapore', 'SGD', 1),
('MYS', 'Malaysia', 'MYR', 1), ('IDN', 'Indonesia', 'IDR', 1), ('PHL', 'Philippines', 'PHP', 1),
('JPN', 'Japan', 'JPY', 1), ('KHM', 'Cambodia', 'KHR', 1), ('LAO', 'Laos', 'LAK', 1),
('MMR', 'Myanmar', 'MMK', 1), ('USA', 'United States', 'USD', 1), ('GBR', 'United Kingdom', 'GBP', 1),
('FRA', 'France', 'EUR', 1), ('DEU', 'Germany', 'EUR', 1), ('AUS', 'Australia', 'AUD', 1),
('CHN', 'China', 'CNY', 1), ('KOR', 'South Korea', 'KRW', 1), ('IND', 'India', 'INR', 1);

INSERT INTO Cities (CityCode, CityName, CountryCode, IsActive) VALUES
('SGN', 'Ho Chi Minh City', 'VNM', 1), ('HAN', 'Hanoi', 'VNM', 1), ('DAD', 'Da Nang', 'VNM', 1),
('CXR', 'Can Tho', 'VNM', 1), ('HUI', 'Hue', 'VNM', 1), ('NHA', 'Nha Trang', 'VNM', 1),
('HPH', 'Hai Phong', 'VNM', 1), ('VCA', 'Vinh', 'VNM', 1), ('PQC', 'Phu Quoc', 'VNM', 1),
('DLI', 'Da Lat', 'VNM', 1), ('BKK', 'Bangkok', 'THA', 1), ('SIN', 'Singapore', 'SGP', 1),
('KUL', 'Kuala Lumpur', 'MYS', 1), ('CGK', 'Jakarta', 'IDN', 1), ('MNL', 'Manila', 'PHL', 1),
('NRT', 'Tokyo', 'JPN', 1), ('PNH', 'Phnom Penh', 'KHM', 1), ('VTE', 'Vientiane', 'LAO', 1),
('RGN', 'Yangon', 'MMR', 1), ('LAX', 'Los Angeles', 'USA', 1), ('JFK', 'New York', 'USA', 1),
('LHR', 'London', 'GBR', 1), ('CDG', 'Paris', 'FRA', 1), ('SYD', 'Sydney', 'AUS', 1), ('PEK', 'Beijing', 'CHN', 1);

-- =============================================
-- STEP 4: INSERT AIRLINES & AIRPORTS
-- =============================================

PRINT '';
PRINT 'STEP 4: Inserting Airlines & Airports...';

DELETE FROM Airports WHERE IATACode IN ('SGN', 'HAN', 'DAD', 'CXR', 'HUI', 'NHA', 'HPH', 'VCA', 'PQC', 'DLI',
                                         'BKK', 'SIN', 'KUL', 'CGK', 'MNL', 'NRT', 'PNH', 'VTE', 'RGN',
                                         'LAX', 'JFK', 'LHR', 'CDG', 'SYD', 'PEK');

DELETE FROM Airlines WHERE IATACode IN ('VN', 'VJ', 'QH', 'TG', 'SQ', 'MH', 'GA', 'JL', 'AA', 'UA', 'BA', 'AF');

INSERT INTO Airports (AirportName, IATACode, CityCode, IsActive) VALUES
('Tan Son Nhat', 'SGN', 'SGN', 1), ('Noi Bai', 'HAN', 'HAN', 1), ('Da Nang International', 'DAD', 'DAD', 1),
('Can Tho International', 'CXR', 'CXR', 1), ('Phu Bai', 'HUI', 'HUI', 1), ('Cam Ranh', 'NHA', 'NHA', 1),
('Cat Bi', 'HPH', 'HPH', 1), ('Vinh Airport', 'VCA', 'VCA', 1), ('Phu Quoc', 'PQC', 'PQC', 1),
('Lien Khuong', 'DLI', 'DLI', 1), ('Suvarnabhumi', 'BKK', 'BKK', 1), ('Changi', 'SIN', 'SIN', 1),
('Kuala Lumpur International', 'KUL', 'KUL', 1), ('Soekarno-Hatta', 'CGK', 'CGK', 1), ('Ninoy Aquino', 'MNL', 'MNL', 1),
('Narita', 'NRT', 'NRT', 1), ('Pochentong', 'PNH', 'PNH', 1), ('Wattay', 'VTE', 'VTE', 1),
('Yangon International', 'RGN', 'RGN', 1), ('Los Angeles International', 'LAX', 'LAX', 1),
('John F Kennedy', 'JFK', 'JFK', 1), ('Heathrow', 'LHR', 'LHR', 1), ('Charles de Gaulle', 'CDG', 'CDG', 1),
('Sydney International', 'SYD', 'SYD', 1), ('Capital Airport', 'PEK', 'PEK', 1);

INSERT INTO Airlines (AirlineName, IATACode, CountryCode, ContactEmail, ContactPhone, Website, IsActive) VALUES
('Vietnam Airlines', 'VN', 'VNM', 'info@vietnamairlines.com', '+84-8-3842-2222', 'vietnamairlines.com', 1),
('Vietjet Air', 'VJ', 'VNM', 'care@vietjet.com', '+84-1900-1886', 'vietjet.com', 1),
('Bamboo Airways', 'QH', 'VNM', 'contact@bambooairways.com', '+84-1900-1000', 'bambooairways.com', 1),
('Thai Airways', 'TG', 'THA', 'contact@thaiairways.com', '+66-2-545-1888', 'thaiairways.com', 1),
('Singapore Airlines', 'SQ', 'SGP', 'info@singaporeair.com', '+65-6223-8888', 'singaporeair.com', 1),
('Malaysia Airlines', 'MH', 'MYS', 'contact@malaysiaairlines.com', '+60-7-303-7722', 'malaysiaairlines.com', 1),
('Garuda Indonesia', 'GA', 'IDN', 'info@garuda-indonesia.com', '+62-21-2351-9999', 'garuda-indonesia.com', 1),
('Japan Airlines', 'JL', 'JPN', 'contact@jal.co.jp', '+81-50-7533-1212', 'jal.co.jp', 1),
('American Airlines', 'AA', 'USA', 'info@aa.com', '+1-800-433-7300', 'aa.com', 1),
('United Airlines', 'UA', 'USA', 'support@united.com', '+1-800-864-8331', 'united.com', 1),
('British Airways', 'BA', 'GBR', 'contact@britishairways.com', '+44-344-222-1111', 'britishairways.com', 1),
('Air France', 'AF', 'FRA', 'info@airfrance.com', '+33-1-4262-2222', 'airfrance.com', 1);

-- =============================================
-- STEP 5: INSERT AIRCRAFT TYPES & AIRCRAFT
-- =============================================

PRINT '';
PRINT 'STEP 5: Inserting Aircraft Types & Aircraft...';

DELETE FROM Seats;
DELETE FROM AircraftSeatConfig;
DELETE FROM Aircraft;
DELETE FROM AircraftTypes;

INSERT INTO AircraftTypes (TypeName, DisplayName) VALUES
('B737', 'Boeing 737-800'), ('B777', 'Boeing 777-300ER'), ('A320', 'Airbus A320-200'),
('A330', 'Airbus A330-300'), ('B787', 'Boeing 787-9 Dreamliner'), ('A380', 'Airbus A380-800');

DECLARE @i INT = 0;
WHILE @i < 30
BEGIN
    INSERT INTO Aircraft (AirlineID, AircraftTypeID, AircraftName) 
    SELECT 1, ((@i % 3) + 1), 'VN-' + FORMAT(@i + 1, '000');
    SET @i = @i + 1;
END;

DECLARE @AirlineLoop INT = 2;
WHILE @AirlineLoop <= 12
BEGIN
    SET @i = 0;
    WHILE @i < (3 + (@AirlineLoop % 3))
    BEGIN
        INSERT INTO Aircraft (AirlineID, AircraftTypeID, AircraftName) 
        SELECT @AirlineLoop, ((@i % 6) + 1), CHAR(65 + @AirlineLoop - 2) + '-' + FORMAT(@i + 1, '00');
        SET @i = @i + 1;
    END;
    SET @AirlineLoop = @AirlineLoop + 1;
END;

-- =============================================
-- STEP 6: INSERT AIRCRAFT SEAT CONFIG & SEATS
-- =============================================

PRINT '';
PRINT 'STEP 6: Inserting Aircraft Seat Config & Seats...';

INSERT INTO AircraftSeatConfig (AircraftID, SeatClassID, SeatCount, RowStart, RowEnd)
SELECT AircraftID, 1, 150, 1, 30 FROM Aircraft
UNION ALL
SELECT AircraftID, 2, 50, 31, 40 FROM Aircraft
UNION ALL
SELECT AircraftID, 3, 30, 41, 46 FROM Aircraft
UNION ALL
SELECT AircraftID, 4, 20, 47, 50 FROM Aircraft;

DECLARE @AircraftID INT;
DECLARE @SeatClassID INT;
DECLARE @RowStart INT;
DECLARE @RowEnd INT;
DECLARE @Row INT;
DECLARE @Col INT;
DECLARE @SeatNumber NVARCHAR(5);

DECLARE aircraft_cursor CURSOR FOR
SELECT AircraftID, SeatClassID, RowStart, RowEnd FROM AircraftSeatConfig;

OPEN aircraft_cursor;
FETCH NEXT FROM aircraft_cursor INTO @AircraftID, @SeatClassID, @RowStart, @RowEnd;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Row = @RowStart;
    WHILE @Row <= @RowEnd
    BEGIN
        SET @Col = 0;
        WHILE @Col < 6
        BEGIN
            INSERT INTO Seats (AircraftID, SeatClassID, SeatNumber, IsAvailable)
            VALUES (@AircraftID, @SeatClassID, CHAR(65 + @Col) + FORMAT(@Row, '00'), 1);
            SET @Col = @Col + 1;
        END;
        SET @Row = @Row + 1;
    END;
    FETCH NEXT FROM aircraft_cursor INTO @AircraftID, @SeatClassID, @RowStart, @RowEnd;
END;

CLOSE aircraft_cursor;
DEALLOCATE aircraft_cursor;

-- =============================================
-- STEP 7: INSERT FLIGHTS & FLIGHT PRICING
-- =============================================

PRINT '';
PRINT 'STEP 7: Inserting Flights & FlightPricing...';

DELETE FROM FlightPricing;
DELETE FROM Flights;

DECLARE @DomesticCount INT = 0;
DECLARE @InternationalCount INT = 0;
DECLARE @DomesticTarget INT = 2100;
DECLARE @InternationalTarget INT = 1400;

DECLARE @DomesticAirports TABLE (AirportID INT);
INSERT INTO @DomesticAirports VALUES (1), (2), (3), (4), (5), (6), (7), (8), (9), (10);

DECLARE @InternationalAirports TABLE (AirportID INT);
INSERT INTO @InternationalAirports VALUES (1), (11), (12), (13), (14), (15), (16), (17), (18), (19), (20), (21), (22), (23), (24), (25);

WHILE @DomesticCount < @DomesticTarget
BEGIN
    DECLARE @FlightDate DATE = CAST(DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 31, '2026-1-20') AS DATE);
    DECLARE @Departure INT = (SELECT TOP 1 AirportID FROM @DomesticAirports ORDER BY NEWID());
    DECLARE @Arrival INT = (SELECT TOP 1 AirportID FROM @DomesticAirports WHERE AirportID != @Departure ORDER BY NEWID());
    DECLARE @AirlineID INT = (SELECT TOP 1 AirlineID FROM Airlines WHERE IATACode IN ('VN', 'VJ', 'QH') ORDER BY NEWID());
    DECLARE @AircraftID_f1 INT = (SELECT TOP 1 AircraftID FROM Aircraft WHERE AirlineID = @AirlineID ORDER BY NEWID());
    DECLARE @DepartureTime TIME = CAST(DATEADD(MINUTE, (ABS(CHECKSUM(NEWID())) % 1440 / 5) * 5, '00:00') AS TIME);
    DECLARE @Duration INT = 60 + (ABS(CHECKSUM(NEWID())) % 151);
    DECLARE @ArrivalTime TIME = DATEADD(MINUTE, @Duration, @DepartureTime);
    DECLARE @BasePrice DECIMAL(12,2) = 1000000 + (ABS(CHECKSUM(NEWID())) % 2000000);
    DECLARE @StatusRand INT = ABS(CHECKSUM(NEWID())) % 100;
    DECLARE @Status NVARCHAR(20) = CASE WHEN @StatusRand < 70 THEN 'Available' WHEN @StatusRand < 85 THEN 'Full' ELSE 'Cancelled' END;
    DECLARE @FlightNumber NVARCHAR(10) = (SELECT IATACode FROM Airlines WHERE AirlineID = @AirlineID) + FORMAT(@DomesticCount % 999 + 100, '000');
    DECLARE @NewFlightID INT;

    INSERT INTO Flights (AirlineID, FlightNumber, AircraftID, DepartureAirportID, ArrivalAirportID, FlightDate, DepartureTime, ArrivalTime, DurationMinutes, Status, BasePrice)
    VALUES (@AirlineID, @FlightNumber, @AircraftID_f1, @Departure, @Arrival, @FlightDate, @DepartureTime, @ArrivalTime, @Duration, @Status, @BasePrice);
    
    SET @NewFlightID = SCOPE_IDENTITY();
    
    -- Insert FlightPricing cho các seat classes
    INSERT INTO FlightPricing (FlightID, SeatClassID, Price, BookedSeats)
    SELECT 
        @NewFlightID,
        sc.SeatClassID,
        @BasePrice * sc.PriceMultiplier,
        0
    FROM SeatClasses sc;

    SET @DomesticCount = @DomesticCount + 1;
    IF @DomesticCount % 500 = 0 PRINT '    Domestic: ' + CAST(@DomesticCount AS NVARCHAR(10));
END;

WHILE @InternationalCount < @InternationalTarget
BEGIN
    SET @FlightDate = CAST(DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 31, '2026-1-20') AS DATE);
    SET @Departure = (SELECT TOP 1 AirportID FROM @InternationalAirports ORDER BY NEWID());
    SET @Arrival = (SELECT TOP 1 AirportID FROM @InternationalAirports WHERE AirportID != @Departure ORDER BY NEWID());
    SET @AirlineID = (SELECT TOP 1 AirlineID FROM Airlines ORDER BY NEWID());
    SET @AircraftID_f1 = (SELECT TOP 1 AircraftID FROM Aircraft WHERE AirlineID = @AirlineID ORDER BY NEWID());
    SET @DepartureTime = CAST(DATEADD(MINUTE, (ABS(CHECKSUM(NEWID())) % 1440 / 5) * 5, '00:00') AS TIME);
    SET @Duration = 180 + (ABS(CHECKSUM(NEWID())) % 721);
    SET @ArrivalTime = DATEADD(MINUTE, @Duration, @DepartureTime);
    SET @BasePrice = 3000000 + (ABS(CHECKSUM(NEWID())) % 5000000);
    SET @StatusRand = ABS(CHECKSUM(NEWID())) % 100;
    SET @Status = CASE WHEN @StatusRand < 70 THEN 'Available' WHEN @StatusRand < 85 THEN 'Full' ELSE 'Cancelled' END;
    SET @FlightNumber = (SELECT IATACode FROM Airlines WHERE AirlineID = @AirlineID) + FORMAT(@InternationalCount % 999 + 100, '000');
    SET @NewFlightID = 0;

    INSERT INTO Flights (AirlineID, FlightNumber, AircraftID, DepartureAirportID, ArrivalAirportID, FlightDate, DepartureTime, ArrivalTime, DurationMinutes, Status, BasePrice)
    VALUES (@AirlineID, @FlightNumber, @AircraftID_f1, @Departure, @Arrival, @FlightDate, @DepartureTime, @ArrivalTime, @Duration, @Status, @BasePrice);
    
    SET @NewFlightID = SCOPE_IDENTITY();
    
    -- Insert FlightPricing
    INSERT INTO FlightPricing (FlightID, SeatClassID, Price, BookedSeats)
    SELECT 
        @NewFlightID,
        sc.SeatClassID,
        @BasePrice * sc.PriceMultiplier,
        0
    FROM SeatClasses sc;

    SET @InternationalCount = @InternationalCount + 1;
    IF @InternationalCount % 500 = 0 PRINT '    International: ' + CAST(@InternationalCount AS NVARCHAR(10));
END;

-- =============================================
-- STEP 8: INSERT SERVICES
-- =============================================

PRINT '';
PRINT 'STEP 8: Inserting Services...';

DELETE FROM BookingServices;
DELETE FROM Services;

INSERT INTO Services (ServiceName, Category, Description, BasePrice, Unit, IsActive) VALUES
('Extra Baggage 20kg', 'Baggage', 'Add extra checked baggage', 300000, 'bag', 1),
('Extra Baggage 10kg', 'Baggage', 'Add extra checked baggage', 150000, 'bag', 1),
('Seat Selection', 'Priority', 'Choose your preferred seat', 50000, 'seat', 1),
('Priority Check-in', 'Priority', 'Fast track check-in', 100000, 'person', 1),
('Priority Boarding', 'Priority', 'Board the flight first', 80000, 'person', 1),
('Meal Upgrade', 'Meal', 'Upgrade meal service', 120000, 'person', 1),
('Travel Insurance', 'Insurance', 'Travel insurance coverage', 250000, 'person', 1),
('Lounge Access', 'Priority', '24-hour lounge access', 200000, 'person', 1),
('WiFi Pass', 'Priority', 'In-flight WiFi pass', 80000, 'person', 1),
('Pet Booking', 'Other', 'Transport pet on board', 500000, 'pet', 1);

-- =============================================
-- STEP 9: INSERT USERS & USER ROLES
-- =============================================

PRINT '';
PRINT 'STEP 9: Inserting Users & UserRoles...';

DELETE FROM UserRoles;
DELETE FROM Users;

-- Admin account
DECLARE @AdminUserID UNIQUEIDENTIFIER = 'd3f9a7c2-8b1e-4f3a-9c2a-7e4f9a1b2c3d';
DECLARE @AdminEmail NVARCHAR(100) = 'adminsystem@gmail.com';

INSERT INTO Users (UserID, FullName, Email, Phone, PasswordHash, DateOfBirth, Gender, CityCode, Address, IsVerified, IsActive, CreatedAt)
VALUES (@AdminUserID, 'Admin System', @AdminEmail, '+84901234567', 
        '$2y$10$zyStMeYVzLEfJgJvTEGH9uqZDuqCmFKprzN2rVkDqqjfPPYhvWqZe', '1980-01-01', 'O', 'SGN', '123 Admin St', 1, 1, '2025-10-20');

DECLARE @AdminRoleID INT = (SELECT RoleID FROM Roles WHERE RoleName = 'Admin');
INSERT INTO UserRoles (UserID, RoleID, AssignedAt) VALUES (@AdminUserID, @AdminRoleID, GETDATE());

CREATE TABLE #UserAccounts (
    Email NVARCHAR(100),
    PlainPassword NVARCHAR(50),
    UserType NVARCHAR(20),
    UserID UNIQUEIDENTIFIER
);

DECLARE @StaffCount INT = 0;
DECLARE @FirstName NVARCHAR(50);
DECLARE @LastName NVARCHAR(50);
DECLARE @Email NVARCHAR(100);
DECLARE @Phone NVARCHAR(20);
DECLARE @PlainPassword NVARCHAR(50);
DECLARE @HashedPassword NVARCHAR(255) = '$2b$10$N9qo8uLOickgx2ZMRZoMyeIjZAgcg7b3XeKeUxWdeS86E36CHqV36';
DECLARE @CityCodeUser CHAR(3);
DECLARE @NewUserID UNIQUEIDENTIFIER;

WHILE @StaffCount < 10
BEGIN
    SET @FirstName = (SELECT TOP 1 FirstName FROM #VietnamNames ORDER BY NEWID());
    SET @LastName = (SELECT TOP 1 LastName FROM #LastNames ORDER BY NEWID());
    SET @Email = 'staff' + FORMAT(@StaffCount + 1, '00') + '@airline.com';
    SET @Phone = '+84' + FORMAT(ABS(CHECKSUM(NEWID())) % 900000000 + 100000000, '0000000000');
    SET @PlainPassword = 'StaffPass@' + FORMAT(@StaffCount + 1, '00');
    SET @CityCodeUser = 'SGN';

    INSERT INTO Users (FullName, Email, Phone, PasswordHash, DateOfBirth, Gender, CityCode, Address, IsVerified, IsActive)
    VALUES (@FirstName + ' ' + @LastName, @Email, @Phone, @HashedPassword, DATEADD(YEAR, -(30 + ABS(CHECKSUM(NEWID())) % 20), CAST(GETDATE() AS DATE)), 
            CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'M' ELSE 'F' END, @CityCodeUser, 'Address Staff ' + FORMAT(@StaffCount + 1, '00'), 1, 1);

    SET @NewUserID = (SELECT TOP 1 UserID FROM Users WHERE Email = @Email);
    INSERT INTO #UserAccounts (Email, PlainPassword, UserType, UserID) VALUES (@Email, @PlainPassword, 'Staff', @NewUserID);

    SET @StaffCount = @StaffCount + 1;
END;

DECLARE @UserCount INT = 0;
DECLARE @IsViet INT;

WHILE @UserCount < 89
BEGIN
    SET @IsViet = ABS(CHECKSUM(NEWID())) % 2;
    
    IF @IsViet = 1
    BEGIN
        SET @FirstName = (SELECT TOP 1 FirstName FROM #VietnamNames ORDER BY NEWID());
        SET @LastName = (SELECT TOP 1 LastName FROM #LastNames WHERE LastName IN ('Nguyễn', 'Trần', 'Phạm', 'Lê', 'Đặng') ORDER BY NEWID());
        SET @Phone = '+84' + FORMAT(ABS(CHECKSUM(NEWID())) % 900000000 + 100000000, '0000000000');
    END
    ELSE
    BEGIN
        SET @FirstName = (SELECT TOP 1 FirstName FROM #InternationalNames ORDER BY NEWID());
        SET @LastName = (SELECT TOP 1 LastName FROM #LastNames WHERE LastName IN ('Smith', 'Johnson', 'Williams', 'Jones', 'Brown') ORDER BY NEWID());
        SET @Phone = '+' + FORMAT(ABS(CHECKSUM(NEWID())) % 9 + 1, '0') + FORMAT(ABS(CHECKSUM(NEWID())) % 9999999999, '0000000000');
    END;
    
    SET @Email = LOWER(@FirstName) + '.' + LOWER(@LastName) + FORMAT(@UserCount + 1, '000') + '@airline.com';
    SET @CityCodeUser = (SELECT TOP 1 CityCode FROM Cities WHERE CountryCode = 'VNM' ORDER BY NEWID());
    SET @PlainPassword = 'Pass@' + FORMAT(@UserCount + 1, '0000');

    INSERT INTO Users (FullName, Email, Phone, PasswordHash, DateOfBirth, Gender, CityCode, Address, IsVerified, IsActive)
    VALUES (@FirstName + ' ' + @LastName, @Email, @Phone, @HashedPassword, DATEADD(YEAR, -(20 + ABS(CHECKSUM(NEWID())) % 50), CAST(GETDATE() AS DATE)), 
            CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'M' ELSE 'F' END, @CityCodeUser, 'Address User ' + FORMAT(@UserCount + 1, '000'), 1, 1);

    SET @NewUserID = (SELECT TOP 1 UserID FROM Users WHERE Email = @Email);
    INSERT INTO #UserAccounts (Email, PlainPassword, UserType, UserID) VALUES (@Email, @PlainPassword, 'User', @NewUserID);

    SET @UserCount = @UserCount + 1;
    IF @UserCount % 20 = 0 PRINT '    Customers: ' + CAST(@UserCount AS NVARCHAR(10));
END;

-- Assign roles
DECLARE @StaffRoleID INT = (SELECT RoleID FROM Roles WHERE RoleName = 'Staff');
INSERT INTO UserRoles (UserID, RoleID, AssignedAt)
SELECT UserID, @StaffRoleID, GETDATE() FROM #UserAccounts WHERE UserType = 'Staff';

DECLARE @CustomerRoleID INT = (SELECT RoleID FROM Roles WHERE RoleName = 'Customer');
INSERT INTO UserRoles (UserID, RoleID, AssignedAt)
SELECT UserID, @CustomerRoleID, GETDATE() FROM #UserAccounts WHERE UserType = 'User';

-- =============================================
-- STEP 10: INSERT BOOKINGS, PASSENGERS, BOOKING FLIGHTS
-- =============================================

PRINT '';
PRINT 'STEP 10: Inserting Bookings, Passengers & BookingFlights...';

DELETE FROM Tickets;
DELETE FROM BookingFlights;
DELETE FROM Passengers;
DELETE FROM Bookings;

DECLARE @TicketCount INT = 0;
DECLARE @TargetTickets INT = 800;
DECLARE @BookingID INT;
DECLARE @BookingReference NVARCHAR(10);
DECLARE @UserIDTicket UNIQUEIDENTIFIER;
DECLARE @FlightIDTicket INT;
DECLARE @PassengerID INT;
DECLARE @SeatClassID_f INT;
DECLARE @TicketStatus NVARCHAR(20);
DECLARE @TicketNumber NVARCHAR(15);
DECLARE @TicketPrice DECIMAL(12,2);
DECLARE @TaxAmt DECIMAL(12,2);
DECLARE @FeeAmt DECIMAL(12,2);
DECLARE @SeatClassMultiplier DECIMAL(4,2);
DECLARE @BasePriceTicket DECIMAL(12,2);
DECLARE @PassengerFirstName NVARCHAR(50);
DECLARE @PassengerLastName NVARCHAR(50);
DECLARE @StatusDistribution INT;
DECLARE @FlightStatusTicket NVARCHAR(20);
DECLARE @BookingFlightID INT;
DECLARE @PassengerType NVARCHAR(10);
DECLARE @PassengerAge INT;
DECLARE @PassengerDOB DATE;

WHILE @TicketCount < @TargetTickets
BEGIN
    SELECT TOP 1 @FlightIDTicket = FlightID, @BasePriceTicket = BasePrice, @FlightStatusTicket = Status 
    FROM Flights WHERE Status = 'Available' ORDER BY NEWID();
    
    IF @FlightIDTicket IS NULL
        SELECT TOP 1 @FlightIDTicket = FlightID, @BasePriceTicket = BasePrice, @FlightStatusTicket = Status FROM Flights ORDER BY NEWID();
    
    SELECT TOP 1 @UserIDTicket = UserID FROM #UserAccounts WHERE UserType = 'User' ORDER BY NEWID();
    SELECT TOP 1 @SeatClassID_f = SeatClassID, @SeatClassMultiplier = PriceMultiplier FROM SeatClasses ORDER BY NEWID();
    
    SET @BookingReference = UPPER(LEFT(CAST(NEWID() AS NVARCHAR(36)), 6));

    -- CALCULATE PRICE FOR BOOKING (Since 1 Booking = 1 Passenger in this data gen logic)
    SET @TicketPrice = @BasePriceTicket * @SeatClassMultiplier;
    SET @TaxAmt = CAST(@TicketPrice * 0.1 AS DECIMAL(10,2));
    SET @FeeAmt = CAST(@TicketPrice * 0.05 AS DECIMAL(10,2));
    
    -- INSERT INTO BOOKINGS WITH PRICE, TAXES, FEES
    INSERT INTO Bookings (BookingReference, UserID, BookingDate, Status, Currency, ContactEmail, ContactPhone, Price, Taxes, Fees)
    SELECT @BookingReference, @UserIDTicket, DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 30, GETDATE()), 'Confirmed', 'VND',
           (SELECT Email FROM Users WHERE UserID = @UserIDTicket),
           (SELECT Phone FROM Users WHERE UserID = @UserIDTicket),
           @TicketPrice, @TaxAmt, @FeeAmt;
    
    SET @BookingID = SCOPE_IDENTITY();
    
    -- Determine passenger type
    SET @StatusDistribution = ABS(CHECKSUM(NEWID())) % 100;
    IF @StatusDistribution < 80
    BEGIN
        SET @PassengerType = 'Adult';
        SET @PassengerAge = 25 + (ABS(CHECKSUM(NEWID())) % 40);
    END
    ELSE IF @StatusDistribution < 95
    BEGIN
        SET @PassengerType = 'Child';
        SET @PassengerAge = 3 + (ABS(CHECKSUM(NEWID())) % 12);
    END
    ELSE
    BEGIN
        SET @PassengerType = 'Infant';
        SET @PassengerAge = ABS(CHECKSUM(NEWID())) % 2;
    END;
    
    SET @PassengerDOB = DATEADD(YEAR, -@PassengerAge, CAST(GETDATE() AS DATE));
    
    -- Determine nationality
    SET @IsViet = ABS(CHECKSUM(NEWID())) % 2;
    IF @IsViet = 1
    BEGIN
        SET @PassengerFirstName = (SELECT TOP 1 FirstName FROM #VietnamNames ORDER BY NEWID());
        SET @PassengerLastName = (SELECT TOP 1 LastName FROM #LastNames WHERE LastName IN ('Nguyễn', 'Trần', 'Phạm', 'Lê', 'Đặng') ORDER BY NEWID());
    END
    ELSE
    BEGIN
        SET @PassengerFirstName = (SELECT TOP 1 FirstName FROM #InternationalNames ORDER BY NEWID());
        SET @PassengerLastName = (SELECT TOP 1 LastName FROM #LastNames WHERE LastName IN ('Smith', 'Johnson', 'Williams', 'Jones', 'Brown') ORDER BY NEWID());
    END;
    
    INSERT INTO Passengers (BookingID, PassengerType, Title, FirstName, LastName, DateOfBirth, Gender, IDNumber)
    VALUES (@BookingID, @PassengerType, 'Mr', @PassengerFirstName, @PassengerLastName, @PassengerDOB, 
            CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 'M' ELSE 'F' END, 
            CASE WHEN @PassengerType = 'Infant' THEN NULL ELSE FORMAT(ABS(CHECKSUM(NEWID())) % 999999999, '000000000') END);
    
    SET @PassengerID = SCOPE_IDENTITY();
    
    INSERT INTO BookingFlights (BookingID, FlightID, TripType) VALUES (@BookingID, @FlightIDTicket, 'OneWay');
    SET @BookingFlightID = SCOPE_IDENTITY();
    
    -- Ticket status
    SET @StatusDistribution = ABS(CHECKSUM(NEWID())) % 100;
    SET @TicketStatus = CASE WHEN @StatusDistribution < 85 THEN 'Issued' WHEN @StatusDistribution < 95 THEN 'CheckedIn' ELSE 'Cancelled' END;
    SET @TicketNumber = FORMAT(ABS(CHECKSUM(NEWID())) % 9999999999, '0000000000');
    
    -- INSERT INTO TICKETS (REMOVED Price, Taxes, Fees)
    INSERT INTO Tickets (BookingFlightID, PassengerID, SeatClassID, TicketNumber, Status)
    VALUES (@BookingFlightID, @PassengerID, @SeatClassID_f, @TicketNumber, @TicketStatus);
    
    SET @TicketCount = @TicketCount + 1;
    IF @TicketCount % 100 = 0 PRINT '    Tickets: ' + CAST(@TicketCount AS NVARCHAR(10));
END;

DECLARE @BookingCountFinal INT;
SELECT @BookingCountFinal = COUNT(*) FROM Bookings;
PRINT '  ✓ Bookings: ' + CAST(@BookingCountFinal AS NVARCHAR(10));

-- =============================================
-- STEP 11: INSERT BOOKING SERVICES
-- =============================================

PRINT '';
PRINT 'STEP 11: Inserting BookingServices...';

INSERT INTO BookingServices (BookingID, ServiceID, PassengerID, Quantity, UnitPrice)
SELECT TOP 300 b.BookingID, (SELECT TOP 1 ServiceID FROM Services ORDER BY NEWID()), p.PassengerID,
       CASE WHEN ABS(CHECKSUM(NEWID())) % 3 = 0 THEN 2 ELSE 1 END,
       (SELECT TOP 1 BasePrice FROM Services ORDER BY NEWID())
FROM Bookings b INNER JOIN Passengers p ON b.BookingID = p.BookingID ORDER BY NEWID();

-- =============================================
-- STEP 12: INSERT PAYMENTS & PAYMENT HISTORY
-- =============================================

PRINT '';
PRINT 'STEP 12: Inserting Payments & PaymentHistory...';

DELETE FROM Payments;
DELETE FROM PaymentHistory;

-- NOTE: Payment Amount is now calculated from Bookings (Price+Tax+Fee) + Services
INSERT INTO Payments (BookingID, PaymentMethod, PaymentProvider, Amount, Currency, Status, TransactionID, ProcessedAt, CompletedAt)
SELECT b.BookingID,
       CASE ABS(CHECKSUM(NEWID())) % 5 WHEN 0 THEN 'Credit Card' WHEN 1 THEN 'Debit Card' WHEN 2 THEN 'E-Wallet' WHEN 3 THEN 'Bank Transfer' ELSE 'Online Payment' END,
       CASE ABS(CHECKSUM(NEWID())) % 4 WHEN 0 THEN 'VNPay' WHEN 1 THEN 'Momo' WHEN 2 THEN 'ZaloPay' ELSE 'ATM' END,
       -- UPDATED CALCULATION:
       ISNULL((b.Price + b.Taxes + b.Fees), 0) +
       ISNULL((SELECT SUM(bs.Quantity * bs.UnitPrice) FROM BookingServices bs WHERE bs.BookingID = b.BookingID), 0),
       'VND',
       CASE WHEN ABS(CHECKSUM(NEWID())) % 100 < 85 THEN 'Completed' WHEN ABS(CHECKSUM(NEWID())) % 100 < 95 THEN 'Pending' ELSE 'Failed' END,
       FORMAT(ABS(CHECKSUM(NEWID())) % 999999999999, '000000000000'),
       DATEADD(MINUTE, -ABS(CHECKSUM(NEWID())) % 1440, GETDATE()),
       CASE WHEN ABS(CHECKSUM(NEWID())) % 100 < 85 THEN DATEADD(MINUTE, 30, DATEADD(MINUTE, -ABS(CHECKSUM(NEWID())) % 1440, GETDATE())) ELSE NULL END
FROM Bookings b;

INSERT INTO PaymentHistory (PaymentID, Status, TransactionTime, Note)
SELECT PaymentID, Status, GETDATE(), 'Payment processed' FROM Payments;

DECLARE @PaymentCountFinal INT;
SELECT @PaymentCountFinal = COUNT(*) FROM Payments;
PRINT '  ✓ Payments: ' + CAST(@PaymentCountFinal AS NVARCHAR(10));

-- =============================================
-- STEP 13: INSERT PROMOTIONS & BOOKING PROMOTIONS
-- =============================================

PRINT '';
PRINT 'STEP 13: Inserting Promotions & BookingPromotions...';

DELETE FROM BookingPromotions;
DELETE FROM Promotions;

DECLARE @PromoCampaignID INT = 0;
DECLARE @CampaignStartDate DATETIME;
DECLARE @CampaignEndDate DATETIME;
DECLARE @PromoCodeBase NVARCHAR(20);

WHILE @PromoCampaignID < 6
BEGIN
    SET @CampaignStartDate = DATEADD(DAY, -15, CAST(GETDATE() AS DATETIME));
    SET @CampaignEndDate = DATEADD(DAY, 45, CAST(GETDATE() AS DATETIME));
    SET @PromoCodeBase = 'CAMPAIGN' + FORMAT(@PromoCampaignID + 1, '0');
    
    INSERT INTO Promotions (PromoCode, PromoName, Description, DiscountType, DiscountValue, MinimumAmount, MaximumDiscount, UsageLimit, UserUsageLimit, ValidFrom, ValidTo, IsActive)
    VALUES (@PromoCodeBase, 'Campaign ' + FORMAT(@PromoCampaignID + 1, '0'), 'Promotion campaign ' + FORMAT(@PromoCampaignID + 1, '0'),
            'Percent', 5 + (ABS(CHECKSUM(NEWID())) % 16), 100000, CAST((5 + (ABS(CHECKSUM(NEWID())) % 16)) * 100000 AS DECIMAL(10,2)),
            200 + (ABS(CHECKSUM(NEWID())) % 100), 5, @CampaignStartDate, @CampaignEndDate, 1);
    
    SET @PromoCampaignID = @PromoCampaignID + 1;
END;

DECLARE @DiscountCount INT = 0;
DECLARE @DiscountPercentage DECIMAL(5,2);
DECLARE @PromoDiscountCode NVARCHAR(20);

WHILE @DiscountCount < 30
BEGIN
    SET @DiscountPercentage = 5 + (ABS(CHECKSUM(NEWID())) % 16);
    SET @PromoDiscountCode = 'DISCOUNT' + FORMAT(@DiscountCount + 1, '000');
    
    INSERT INTO Promotions (PromoCode, PromoName, Description, DiscountType, DiscountValue, MinimumAmount, UsageLimit, UserUsageLimit, ValidFrom, ValidTo, IsActive)
    VALUES (@PromoDiscountCode, 'Discount ' + FORMAT(@DiscountCount + 1, '000'), 'Special discount promotion', 'Percent',
            @DiscountPercentage, 50000, 100 + (ABS(CHECKSUM(NEWID())) % 200), 3,
            DATEADD(DAY, -10, CAST(GETDATE() AS DATETIME)), DATEADD(DAY, 30, CAST(GETDATE() AS DATETIME)), 1);
    
    SET @DiscountCount = @DiscountCount + 1;
END;

-- UPDATED LOGIC: Calculate discount based on Booking Price
INSERT INTO BookingPromotions (BookingID, PromotionID, DiscountAmount, AppliedAt)
SELECT TOP 250 b.BookingID, (SELECT TOP 1 PromotionID FROM Promotions WHERE IsActive = 1 ORDER BY NEWID()),
       CAST(ISNULL((b.Price + b.Taxes + b.Fees) * 0.1, 0) AS DECIMAL(10,2)),
       DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 20, GETDATE())
FROM Bookings b WHERE b.Status = 'Confirmed' ORDER BY NEWID();

-- =============================================
-- STEP 14: INSERT NOTIFICATIONS
-- =============================================

PRINT '';
PRINT 'STEP 14: Inserting Notifications...';

DELETE FROM Notifications;

INSERT INTO Notifications (UserID, Title, Message, Type, Channel, RelatedBookingID, IsRead, SentAt, CreatedAt)
SELECT TOP 200 b.UserID, 'Booking Confirmation',
       'Your flight booking ' + b.BookingReference + ' has been confirmed. Flight Date: ' + CONVERT(VARCHAR(10), f.FlightDate, 103),
       'Booking', 'Email', b.BookingID, CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 1 ELSE 0 END,
       b.BookingDate, b.BookingDate
FROM Bookings b INNER JOIN BookingFlights bf ON b.BookingID = bf.BookingID INNER JOIN Flights f ON bf.FlightID = f.FlightID ORDER BY NEWID();

INSERT INTO Notifications (UserID, Title, Message, Type, Channel, RelatedBookingID, IsRead, SentAt, CreatedAt)
SELECT TOP 200 b.UserID, 'Payment Received',
       'Payment for booking ' + b.BookingReference + ' has been received. Thank you!',
       'Payment', 'Email', b.BookingID, CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 1 ELSE 0 END,
       DATEADD(HOUR, 1, b.BookingDate), DATEADD(HOUR, 1, b.BookingDate)
FROM Bookings b INNER JOIN Payments p ON b.BookingID = p.BookingID WHERE p.Status = 'Completed' ORDER BY NEWID();

INSERT INTO Notifications (UserID, Title, Message, Type, Channel, RelatedBookingID, IsRead, SentAt, CreatedAt)
SELECT TOP 300 b.UserID, 'Flight Reminder',
       'Reminder: Your flight ' + f.FlightNumber + ' departs on ' + CONVERT(VARCHAR(10), f.FlightDate, 103) + ' at ' + CONVERT(VARCHAR(5), f.DepartureTime, 108),
       'Flight', 'Email', b.BookingID, 0, DATEADD(DAY, -1, f.FlightDate), DATEADD(DAY, -1, f.FlightDate)
FROM Bookings b INNER JOIN BookingFlights bf ON b.BookingID = bf.BookingID INNER JOIN Flights f ON bf.FlightID = f.FlightID
WHERE f.FlightDate >= CAST(GETDATE() AS DATE) ORDER BY NEWID();

INSERT INTO Notifications (UserID, Title, Message, Type, Channel, RelatedBookingID, IsRead, SentAt, CreatedAt)
SELECT TOP 150 b.UserID, 'Promotion Offer',
       'Special promotion available! Get up to 20% discount on your next flight booking.',
       'Promotion', 'Email', b.BookingID, CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN 1 ELSE 0 END,
       GETDATE(), GETDATE()
FROM Bookings b ORDER BY NEWID();

-- =============================================
-- STEP 15: INSERT AUDIT LOGS
-- =============================================

PRINT '';
PRINT 'STEP 15: Inserting AuditLogs...';

DELETE FROM AuditLogs;

DECLARE @AdminIDForAudit UNIQUEIDENTIFIER = 'd3f9a7c2-8b1e-4f3a-9c2a-7e4f9a1b2c3d';

INSERT INTO AuditLogs (UserID, TableName, Operation, RecordID, OldValues, NewValues, Timestamp, IPAddress, UserAgent)
SELECT TOP 150 @AdminIDForAudit,
       CASE ABS(CHECKSUM(NEWID())) % 6 WHEN 0 THEN 'Flights' WHEN 1 THEN 'Bookings' WHEN 2 THEN 'Payments' WHEN 3 THEN 'Users' WHEN 4 THEN 'Promotions' ELSE 'Tickets' END,
       CASE ABS(CHECKSUM(NEWID())) % 3 WHEN 0 THEN 'INSERT' WHEN 1 THEN 'UPDATE' ELSE 'DELETE' END,
       FORMAT(ABS(CHECKSUM(NEWID())) % 5000, '0000'), '{}', '{"Status":"Active"}',
       DATEADD(MINUTE, -ABS(CHECKSUM(NEWID())) % 2880, GETDATE()),
       '192.168.' + CAST(ABS(CHECKSUM(NEWID())) % 255 AS VARCHAR(3)) + '.' + CAST(ABS(CHECKSUM(NEWID())) % 255 AS VARCHAR(3)),
       'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36'
FROM Bookings ORDER BY NEWID();

-- =============================================
-- FINAL SUMMARY
-- =============================================

PRINT N'';
PRINT N'==============================';
PRINT N'   ✓ DATA IMPORT COMPLETE';
PRINT N'==============================';
PRINT N'';

DROP TABLE #VietnamNames;
DROP TABLE #InternationalNames;
DROP TABLE #LastNames;
DROP TABLE #UserAccounts;

-- =============================================
-- DATA SUMMARY REPORT
-- =============================================

PRINT N'';
PRINT N'==============================';
PRINT N'      DATABASE SUMMARY REPORT';
PRINT N'==============================';
PRINT N'';

PRINT 'CORE DATA STATISTICS:';
PRINT '---';

DECLARE @TotalFlights INT = (SELECT COUNT(*) FROM Flights);
DECLARE @TotalBookings INT = (SELECT COUNT(*) FROM Bookings);
DECLARE @TotalTickets INT = (SELECT COUNT(*) FROM Tickets);
DECLARE @TotalUsers INT = (SELECT COUNT(*) FROM Users);
DECLARE @TotalPayments INT = (SELECT COUNT(*) FROM Payments);

PRINT '  • Total Flights: ' + CAST(@TotalFlights AS NVARCHAR(10));
PRINT '  • Total Bookings: ' + CAST(@TotalBookings AS NVARCHAR(10));
PRINT '  • Total Tickets: ' + CAST(@TotalTickets AS NVARCHAR(10));
PRINT '  • Total Users: ' + CAST(@TotalUsers AS NVARCHAR(10));
PRINT '  • Total Payments: ' + CAST(@TotalPayments AS NVARCHAR(10));
PRINT N'';

-- Flight Distribution
PRINT 'FLIGHT DISTRIBUTION BY STATUS:';
PRINT '---';
SELECT 
    Status,
    COUNT(*) as FlightCount,
    CAST(ROUND(COUNT(*) * 100.0 / @TotalFlights, 2) AS NVARCHAR(10)) + '%' as Percentage
FROM Flights
GROUP BY Status;

PRINT N'';

-- Booking Distribution
PRINT 'BOOKING DISTRIBUTION BY STATUS:';
PRINT '---';
SELECT 
    Status,
    COUNT(*) as BookingCount,
    CAST(ROUND(COUNT(*) * 100.0 / @TotalBookings, 2) AS NVARCHAR(10)) + '%' as Percentage
FROM Bookings
GROUP BY Status;

PRINT N'';

-- Ticket Distribution (Modified to check Status only, as Price is gone)
PRINT 'TICKET DISTRIBUTION BY STATUS:';
PRINT '---';
SELECT 
    Status,
    COUNT(*) as TicketCount,
    CAST(ROUND(COUNT(*) * 100.0 / @TotalTickets, 2) AS NVARCHAR(10)) + '%' as Percentage
FROM Tickets
GROUP BY Status;

PRINT N'';

-- User Distribution
PRINT 'USER DISTRIBUTION BY ROLE:';
PRINT '---';
SELECT 
    r.RoleName,
    COUNT(*) as UserCount,
    CAST(ROUND(COUNT(*) * 100.0 / @TotalUsers, 2) AS NVARCHAR(10)) + '%' as Percentage
FROM Users u
INNER JOIN UserRoles ur ON u.UserID = ur.UserID
INNER JOIN Roles r ON ur.RoleID = r.RoleID
GROUP BY r.RoleName;

PRINT N'';

-- Airlines with most flights
PRINT 'TOP 5 AIRLINES BY FLIGHT COUNT:';
PRINT '---';
SELECT TOP 5
    a.AirlineName,
    COUNT(f.FlightID) as FlightCount,
    COUNT(DISTINCT f.FlightDate) as OperatingDays
FROM Flights f
INNER JOIN Airlines a ON f.AirlineID = a.AirlineID
GROUP BY a.AirlineName
ORDER BY FlightCount DESC;

PRINT N'';
PRINT N'==============================';
PRINT N'   DATA IMPORT FINISHED';
PRINT N'==============================';