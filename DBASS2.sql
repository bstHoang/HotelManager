-- T?o b?ng Customer
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY,
    CustomerFullName NVARCHAR(255),
    Telephone NVARCHAR(50),
    EmailAddress NVARCHAR(255),
    CustomerBirthday DATE,
    CustomerStatus NVARCHAR(50),
    Password NVARCHAR(255)
);

-- T?o b?ng RoomType
CREATE TABLE RoomType (
    RoomTypeID INT PRIMARY KEY,
    RoomTypeName NVARCHAR(255),
    TypeDescription NVARCHAR(255),
    TypeNote NVARCHAR(255)
);

-- T?o b?ng RoomInformation
CREATE TABLE RoomInformation (
    RoomID INT PRIMARY KEY,
    RoomNumber NVARCHAR(50),
    RoomDetailDescription NVARCHAR(255),
    RoomMaxCapacity INT,
    RoomTypeID INT,
    RoomStatus NVARCHAR(50),
    RoomPricePerDay DECIMAL(18, 2),
    FOREIGN KEY (RoomTypeID) REFERENCES RoomType(RoomTypeID)
);

-- T?o b?ng BookingReservation
CREATE TABLE BookingReservation (
    BookingReservationID INT PRIMARY KEY,
    BookingDate DATE,
    TotalPrice DECIMAL(18, 2),
    CustomerID INT,
    BookingStatus NVARCHAR(50),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
use PRN_Ass2
INSERT INTO BookingReservation (BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus)
VALUES 
(2, '2024-07-22', 200.00, 124, 'Pending'),
(3, '2024-07-23', 300.00, 125, 'Cancelled'),
(4, '2024-07-24', 250.00, 126, 'Confirmed');

-- T?o b?ng BookingDetail
CREATE TABLE BookingDetail (
    BookingReservationID INT,
    RoomID INT,
    StartDate DATE,
    EndDate DATE,
    ActualPrice DECIMAL(18, 2),
    PRIMARY KEY (BookingReservationID, RoomID),
    FOREIGN KEY (BookingReservationID) REFERENCES BookingReservation(BookingReservationID),
    FOREIGN KEY (RoomID) REFERENCES RoomInformation(RoomID)
);
