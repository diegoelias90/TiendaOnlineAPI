CREATE DATABASE TiendaOnline;
GO
USE TiendaOnline;
GO
CREATE TABLE Categorias(
CategoriaId INT PRIMARY KEY IDENTITY(1,1),
Nombre NVARCHAR(100) NOT NULL
)
CREATE TABLE Productos(
ProductoId INT PRIMARY KEY IDENTITY(1,1),
Nombre NVARCHAR(200) NOT NULL, 
Descripcion NVARCHAR(500) NOT NULL, 
Precio DECIMAL(10,2) NOT NULL, 
UrlImagen NVARCHAR (500),
CategoriaId INT FOREIGN KEY REFERENCES Categorias(CategoriaId)
);
CREATE TABLE Pedidos(
PedidoId INT PRIMARY KEY IDENTITY(1,1),
FechaPedido DATETIME DEFAULT GETDATE(),
Cliente NVARCHAR(200) NOT NULL
);
CREATE TABLE PedidoDetalle(
DetalleId INT PRIMARY KEY IDENTITY(1,1),
PedidoId INT FOREIGN KEY REFERENCES Pedidos(PedidoId),
ProductoId INT FOREIGN KEY REFERENCES Productos(ProductoId),
Cantidad INT NOT NULL, 
PrecioUnitario DECIMAL(10,2) NOT NULL
);
GO
INSERT INTO Categorias(Nombre) VALUES ('Electrónica'), ('Ropa'), ('Hogar');
INSERT INTO Productos(Nombre, Descripcion, Precio, UrlImagen, CategoriaId) VALUES 
('Laptop Dell', 'Laptop de 15 pulgadas', 850.00, 'https://example.com/laptop.jpg', 1),
('Camiseta negra', 'Camiseta de algodón', 15.00, 'https://example.com/camiseta.jpg', 2),
('Silla Gamer', 'Silla ergonómica para juegos', 120.00, 'https://example.com/silla.jpg', 3);
