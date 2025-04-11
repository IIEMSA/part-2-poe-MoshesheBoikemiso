-- DROP existing objects if they exist
DROP TRIGGER IF EXISTS PreventDoubleBooking;
DROP TABLE IF EXISTS Bookings;
DROP TABLE IF EXISTS EventEntity;
DROP TABLE IF EXISTS Venues;
GO

-- CREATE Venues table
CREATE TABLE Venues(
    VenueID INT IDENTITY(1,1) PRIMARY KEY,
    VenueName NVARCHAR(100) NOT NULL,
    VenueLocation NVARCHAR(100) NOT NULL,
    VenueCapacity INT NOT NULL
);
GO

-- CREATE EventEntity table with a CHECK constraint
CREATE TABLE EventEntity(
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventName NVARCHAR(100) NOT NULL,
    EventStartDate DATETIME NOT NULL,
    EventEndDate DATETIME NOT NULL,
    CONSTRAINT EventDates CHECK (EventStartDate < EventEndDate)
);
GO

-- CREATE Bookings table
CREATE TABLE Bookings(
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    BookingPrice DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    EventID INT NOT NULL,
    VenueID INT NOT NULL,
    BookingDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (EventID) REFERENCES EventEntity(EventID),
    FOREIGN KEY (VenueID) REFERENCES Venues(VenueID)
);
GO

-- CREATE trigger to prevent double booking
CREATE TRIGGER PreventDoubleBooking
ON Bookings
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(
        SELECT 1
        FROM inserted i
        JOIN EventEntity e1 ON i.EventID = e1.EventID
        JOIN Bookings b ON i.VenueID = b.VenueID
        JOIN EventEntity e2 ON b.EventID = e2.EventID
        WHERE i.BookingID <> b.BookingID
          AND e1.EventStartDate < e2.EventEndDate
          AND e2.EventStartDate < e1.EventEndDate
    )
    BEGIN
        RAISERROR('Double booking detected: overlapping event times for the same venue are not allowed.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- INSERT sample data into Venues
INSERT INTO Venues(VenueName, VenueLocation, VenueCapacity)
VALUES
('Hall of Fame', 'Sun Park', 1000),
('Power', 'Dark River', 800);
GO

-- INSERT sample data into EventEntity
INSERT INTO EventEntity(EventName, EventStartDate, EventEndDate)
VALUES
('Paintball', '2025-01-16 15:00', '2025-01-16 16:00'),
('Tournament', '2025-02-04 09:00', '2025-02-04 16:00');
GO

-- INSERT sample data into Bookings
INSERT INTO Bookings(VenueID, EventID, BookingPrice)
VALUES
(1, 1, 300.00),
(2, 2, 500.00);
GO
