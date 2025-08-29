
-- Database: LibraryManagement

-- Drop tables if they exist
DROP TABLE IF EXISTS Borrowings;
DROP TABLE IF EXISTS Books;
DROP TABLE IF EXISTS Authors;
DROP TABLE IF EXISTS Members;

-- Authors table
CREATE TABLE Authors (
    AuthorId INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    BirthDate DATE
);

-- Books table
CREATE TABLE Books (
    BookId INT PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    PublishedYear INT,
    Price DECIMAL(10,2),
    AuthorId INT,
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

-- Members table
CREATE TABLE Members (
    MemberId INT PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    JoinDate DATE
);

-- Borrowings table
CREATE TABLE Borrowings (
    BorrowingId INT PRIMARY KEY,
    MemberId INT,
    BookId INT,
    BorrowedDate DATE,
    ReturnedDate DATE NULL,
    FOREIGN KEY (MemberId) REFERENCES Members(MemberId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);
