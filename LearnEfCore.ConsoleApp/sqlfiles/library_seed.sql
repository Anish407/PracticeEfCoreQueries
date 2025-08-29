
-- Seed data

-- Authors
INSERT INTO Authors VALUES
(1, 'George Orwell', '1903-06-25'),
(2, 'J.K. Rowling', '1965-07-31'),
(3, 'J.R.R. Tolkien', '1892-01-03'),
(4, 'Haruki Murakami', '1949-01-12'),
(5, 'Chimamanda Ngozi Adichie', '1977-09-15');

-- Books
INSERT INTO Books VALUES
(1, '1984', 1949, 15.99, 1),
(2, 'Animal Farm', 1945, 9.99, 1),
(3, 'Harry Potter and the Sorcerer''s Stone', 1997, 29.99, 2),
(4, 'Harry Potter and the Chamber of Secrets', 1998, 24.99, 2),
(5, 'The Hobbit', 1937, 19.99, 3),
(6, 'The Lord of the Rings', 1954, 49.99, 3),
(7, 'Kafka on the Shore', 2002, 22.99, 4),
(8, 'Norwegian Wood', 1987, 18.99, 4),
(9, 'Half of a Yellow Sun', 2006, 21.99, 5);

-- Members
INSERT INTO Members VALUES
(1, 'Alice Johnson', '2021-01-15'),
(2, 'Bob Smith', '2020-03-10'),
(3, 'Charlie Brown', '2023-07-22'),
(4, 'Diana Prince', '2024-05-05'),
(5, 'Ethan Hunt', '2022-09-30');

-- Borrowings
INSERT INTO Borrowings VALUES
(1, 1, 1, '2024-01-10', '2024-02-01'),
(2, 2, 3, '2024-02-15', NULL),
(3, 3, 5, '2024-03-05', '2024-04-01'),
(4, 1, 3, '2024-04-20', '2024-05-15'),
(5, 4, 7, '2024-06-01', NULL),
(6, 5, 2, '2024-06-10', '2024-07-01'),
(7, 2, 6, '2024-07-15', '2024-08-20'),
(8, 3, 3, '2024-08-05', '2024-09-01'),
(9, 1, 9, '2024-09-12', NULL),
(10, 5, 8, '2024-09-20', '2024-10-10'),
(11, 4, 6, '2024-10-05', NULL),
(12, 2, 4, '2024-10-15', '2024-11-01'),
(13, 3, 1, '2024-11-10', NULL),
(14, 5, 5, '2024-11-15', '2024-12-01'),
(15, 1, 7, '2024-12-20', NULL);
