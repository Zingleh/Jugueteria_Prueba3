CREATE TABLE [Usuario] (
  [id_usuario] int PRIMARY KEY IDENTITY(1, 1),
  [rut] varchar(10),
  [nombre] varchar(30),
  [apellido] varchar(30),
  [fono] varchar(12),
  [direccion] varchar(50)
)
GO

CREATE TABLE [Rol] (
  [id_rol] nvarchar(255) PRIMARY KEY,
  [nombre] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Rol_Usuario] (
  [id_rolUsuario] nvarchar(255) PRIMARY KEY IDENTITY(1, 1),
  [id_rol] nvarchar(255) NOT NULL,
  [id_usuario] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Proveedor] (
  [id_proveedor] nvarchar(255) PRIMARY KEY IDENTITY(1, 1),
  [rut] varchar(10),
  [nombre] varchar(30),
  [apellido] varchar(30),
  [fono] varchar(12),
  [direccion] varchar(50)
)
GO

CREATE TABLE [Juguete] (
  [id_juguete] nvarchar(255) PRIMARY KEY IDENTITY(1, 1),
  [nombre] nvarchar(255) NOT NULL,
  [marca] nvarchar(255) NOT NULL,
  [precioUnit] int NOT NULL
)
GO

CREATE TABLE [Boleta] (
  [id_boleta] nvarchar(255) PRIMARY KEY IDENTITY(1, 1),
  [fecha_emision] date NOT NULL,
  [total] int NOT NULL,
  [id_usuario] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Detalle_boleta] (
  [cantidad_productos] int NOT NULL,
  [id_boleta] nvarchar(255),
  [id_juguete] nvarchar(255),
  PRIMARY KEY ([id_boleta], [id_juguete])
)
GO

ALTER TABLE [Rol_Usuario] ADD FOREIGN KEY ([id_usuario]) REFERENCES [Usuario] ([id_usuario])
GO

ALTER TABLE [Rol_Usuario] ADD FOREIGN KEY ([id_rol]) REFERENCES [Rol] ([id_rol])
GO

ALTER TABLE [Detalle_boleta] ADD FOREIGN KEY ([id_boleta]) REFERENCES [Boleta] ([id_boleta])
GO

ALTER TABLE [Detalle_boleta] ADD FOREIGN KEY ([id_juguete]) REFERENCES [Juguete] ([id_juguete])
GO

ALTER TABLE [Boleta] ADD FOREIGN KEY ([id_usuario]) REFERENCES [Usuario] ([id_usuario])
GO

