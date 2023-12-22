DROP DATABASE IF EXISTS BankDB;
CREATE DATABASE BankDB;
Use BankDB;

DROP TABLE IF EXISTS `Customer`;
CREATE TABLE Customer(
    CustomerID INT UNSIGNED PRIMARY KEY AUTO_INCREMENT UNIQUE,
    Email VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(20) NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    PostalCode VARCHAR(7) NOT NULL,
    Address VARCHAR(320) NOT NULL,
    PhoneNumber INT(10) NOT NULL UNIQUE
);

DROP TABLE IF EXISTS `Account`;
CREATE TABLE Account (
    AccountID INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    AccountNumber INT(7) UNSIGNED NOT NULL UNIQUE,
    CustomerID INT UNSIGNED,
    Balance DECIMAL(10, 2),
    AccountType ENUM('chequing', 'saving'),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);


DROP TABLE IF EXISTS `Transaction`;
CREATE TABLE `Transaction` (
    TransactionID INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    SenderAccountNumber INT(7) UNSIGNED,
    ReceiverAccountNumber  INT(7) UNSIGNED,
    TransactionType ENUM('deposit', 'withdraw', 'transfer'),
    Amount DECIMAL(10, 2) UNSIGNED,
    TransactionDate DATETIME,
    FOREIGN KEY (SenderAccountNumber) REFERENCES Account(AccountNumber),
    FOREIGN KEY (ReceiverAccountNumber) REFERENCES Account(AccountNumber)
);

DROP TABLE IF EXISTS `Admin`;
CREATE TABLE `Admin` (
    AdminID INT PRIMARY KEY AUTO_INCREMENT,
    Username INT UNIQUE,
    Password varchar(16),
    name varchar (50)
);

INSERT INTO `Admin` (Username, Password, Name) VALUES (123456, '123456', 'AdminName');

INSERT INTO Customer (Email, Password, FirstName, LastName, DateOfBirth, PostalCode, Address, PhoneNumber)
VALUES
    ('TestEmail@gmail.com', 'password1', 'John', 'Doe', '1990-05-15', 'A1B 2C3', '123 Main St, Cityville' ,'+1234567890');

INSERT INTO `Account` (AccountNumber, CustomerID, Balance, AccountType)
VALUES
	(1000001, 1, 1000.0, 'chequing'),
    (2000001, 1, 2000.0, 'saving'),
    (1000002, 2, 1570.0, 'chequing'),
    (2000002, 2, 6500.0, 'saving'),
    (1000003, 3, 200.0, 'chequing'),
    (2000003, 3, 0.0, 'saving'),
    (1000004, 4, 3895.0, 'chequing'),
    (2000004, 4, 10025.0, 'saving'),
    (1000005, 5, 1000.0, 'chequing'),
    (2000005, 5, 220.0, 'saving');

INSERT INTO `Transaction` (SenderAccountNumber, ReceiverAccountNumber, TransactionType, Amount, TransactionDate)
VALUES
	(1000001, 1000001, 'deposit', 200.0, '2023-12-04 6:52:10');