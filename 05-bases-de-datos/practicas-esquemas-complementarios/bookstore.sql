
-- CREACIÓN DE BASE DE DATOS
CREATE DATABASE BOOKSTORE;
GO
USE BOOKSTORE;
GO

-- TABLA AUTORES
CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50)
);

-- TABLA EDITORIALES
CREATE TABLE Publishers (
    PublisherID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Country VARCHAR(50)
);

-- TABLA LIBROS
CREATE TABLE Books (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(100),
    AuthorID INT FOREIGN KEY REFERENCES Authors(AuthorID),
    PublisherID INT FOREIGN KEY REFERENCES Publishers(PublisherID),
    Price DECIMAL(10, 2),
    PublicationDate DATE
);

-- TABLA CLIENTES
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100)
);

-- TABLA PEDIDOS
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderDate DATE,
    DocumentType VARCHAR(20),
    TaxRate DECIMAL(5, 2)
);

-- TABLA DETALLE DE PEDIDOS
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    BookID INT FOREIGN KEY REFERENCES Books(BookID),
    Quantity INT,
    UnitPrice DECIMAL(10, 2)
);

-- INSERCIONES
INSERT INTO Authors (FirstName, LastName) VALUES
('Gabriel', 'García Márquez'),
('Isabel', 'Allende'),
('Mario', 'Vargas Llosa');

INSERT INTO Publishers (Name, Country) VALUES
('Editorial Planeta', 'España'),
('Alfaguara', 'Chile');

INSERT INTO Books (Title, AuthorID, PublisherID, Price, PublicationDate) VALUES
('Cien Años de Soledad', 1, 1, 25.00, '2010-05-20'),
('La Casa de los Espíritus', 2, 2, 20.00, '2012-09-15'),
('La Fiesta del Chivo', 3, 1, 30.00, '2015-11-10');

INSERT INTO Customers (FirstName, LastName, Email) VALUES
('Juan', 'Pérez', 'juan.perez@email.com'),
('Ana', 'López', 'ana.lopez@email.com'),
('Luis', 'Martínez', 'luis.martinez@email.com');

INSERT INTO Orders (CustomerID, OrderDate, DocumentType, TaxRate) VALUES
(1, '2023-01-10', 'Boleta', 18.00),
(1, '2023-02-15', 'Factura', 21.00),
(2, '2023-03-20', 'Boleta', 19.00),
(3, '2023-04-05', 'Factura', 22.00);

INSERT INTO OrderDetails (OrderID, BookID, Quantity, UnitPrice) VALUES
(1, 1, 2, 25.00),
(2, 2, 1, 20.00),
(3, 3, 3, 30.00),
(4, 1, 1, 25.00),
(4, 2, 2, 20.00);
