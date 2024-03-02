USE master;
go
IF (db_id('LRRVG') is null)
  CREATE DATABASE LRRVG;
go  
USE LRRVG; 
go

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Articulo_ArticulosXTienda')AND parent_object_id = OBJECT_ID(N'dbo.ArticulosXTienda'))
  ALTER TABLE ArticulosXTienda DROP CONSTRAINT FK_Articulo_ArticulosXTienda

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Tienda_ArticulosXTienda')AND parent_object_id = OBJECT_ID(N'dbo.ArticulosXTienda'))
  ALTER TABLE ArticulosXTienda DROP CONSTRAINT FK_Tienda_ArticulosXTienda

  
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Cliente_Ventas')AND parent_object_id = OBJECT_ID(N'dbo.Ventas'))
  ALTER TABLE Ventas DROP CONSTRAINT FK_Cliente_Ventas

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Articulo_Ventas')AND parent_object_id = OBJECT_ID(N'dbo.Ventas'))
  ALTER TABLE Ventas DROP CONSTRAINT FK_Articulo_Ventas

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'FK_Tienda_Ventas')AND parent_object_id = OBJECT_ID(N'dbo.Ventas'))
  ALTER TABLE Ventas DROP CONSTRAINT FK_Tienda_Ventas

go
if OBJECT_ID('Clientes','u')is not null
drop table Clientes
go
create table Clientes(
	IdCliente int identity(1,1),
	Nombre varchar(20) NOT NULL,
	Apellidos varchar(20),
	Direccion varchar(20),
	Correo	  varchar(50) not null,
	Passwd	  varchar(max) not null,
	PRIMARY KEY (IdCliente)
)
go
insert into Clientes (nombre,Apellidos,Direccion,Correo,Passwd)
values('admin','admin','','admin','21232F297A57A5A743894A0E4A801FC3')
go
if OBJECT_ID('Tienda','u')is not null
drop table Tienda
go
create table Tienda(
	IdTienda int identity(1,1),
	Sucursal varchar(20) NOT NULL,
	Direccion varchar(20),
	PRIMARY KEY (IdTienda)
)
go
if OBJECT_ID('Articulos','u')is not null
drop table Articulos
go
create table Articulos(
	Codigo varchar(20) not null,
	Descripcion varchar(50) NOT NULL,
	Precio decimal(12,4),
	Stock int,
	PRIMARY KEY (Codigo)
)
go
if OBJECT_ID('ArticulosXTienda','u')is not null
drop table ArticulosXTienda
go
create table ArticulosXTienda(
	Codigo varchar(20) not null,
	IdTienda int,
	CONSTRAINT FK_Articulo_ArticulosXTienda FOREIGN KEY (Codigo) REFERENCES Articulos(Codigo),
	CONSTRAINT FK_Tienda_ArticulosXTienda FOREIGN KEY (IdTienda) REFERENCES Tienda(IdTienda)
)
go
if OBJECT_ID('Ventas','u')is not null
drop table Ventas
go
create table Ventas(
	NumeroVenta int identity(1,1),
	IdTienda int not null,
	IdCliente int not null,
	Codigo varchar(20) not null,
	Precio decimal(12,4)not null,
	Cantidad int not null,
	FechaVenta datetime,
	PRIMARY KEY (NumeroVenta),
	CONSTRAINT FK_Cliente_Ventas FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
	CONSTRAINT FK_Tienda_Ventas FOREIGN KEY (IdTienda) REFERENCES Tienda(IdTienda),
	CONSTRAINT FK_Articulo_Ventas FOREIGN KEY (Codigo) REFERENCES Articulos(Codigo)
)
if OBJECT_ID('AdministraTienda','P')is not null
drop Procedure AdministraTienda
go
Create Procedure AdministraTienda 
(@IdTienda int=null,@Sucursal varchar(20)=null,@Direccion  varchar(20)=null,@Accion varchar(1))
as
IF @Accion='A'
begin
	if(@IdTienda=0)
	begin
		insert into Tienda (Sucursal,Direccion)
		values (@Sucursal,@Direccion)
		SELECT SCOPE_IDENTITY()
	end
	else
	begin
		update Tienda set Sucursal=@Sucursal,Direccion=@Direccion where IdTienda=@IdTienda
		SELECT @IdTienda
	end
End
IF @Accion='B'
begin
		delete Tienda where IdTienda=@IdTienda
		select @@ROWCOUNT
End
IF @Accion='C'
begin
		Select IdTienda,Sucursal,Direccion from Tienda where IdTienda=@IdTienda
End
IF @Accion='T'
begin
		Select IdTienda,Sucursal,Direccion from Tienda 
End
go
if OBJECT_ID('AdministraCliente','P')is not null
drop Procedure AdministraCliente
go
Create Procedure AdministraCliente 
(@IdCliente int=null,@Nombre varchar(20)=null,@Apellidos varchar(20)=null,@Direccion varchar(20)=null,@Correo varchar(50)=null,@Passwd varchar(max)=null,@Accion varchar(1))
as
IF @Accion='A'
begin

		if(@IdCliente>0)
		begin
			if not exists(select 1 from Clientes where Correo=@Correo and IdCliente<>@IdCliente)
			begin
				update Clientes set Nombre=@Nombre,Apellidos=@Apellidos,Direccion=@Direccion,Correo=@Correo,Passwd=@Passwd where IdCliente =@IdCliente 
				select @@ROWCOUNT
			end
			else
				Select -1
		end
		else
		begin
			if not exists(select 1 from Clientes where Correo=@Correo)
			begin
					insert into Clientes (Nombre,Apellidos,Direccion,Correo,Passwd)
					values (@Nombre,@Apellidos,@Direccion,@Correo,@Passwd)
					select @@ROWCOUNT
			end
			else
					Select -1	
		end
End
IF @Accion='B'
begin
		delete Clientes where IdCliente=@IdCliente
		select @@ROWCOUNT
End
IF @Accion='C'
begin
		Select IdCliente,Nombre,Apellidos,Direccion,Correo from Clientes where IdCliente =@IdCliente 
End
IF @Accion='T'
begin
		Select IdCliente,Nombre,Apellidos,Direccion,Correo from Clientes
End
if @Accion='L'
begin
	Select IdCliente,Nombre,Apellidos,Direccion,Correo from Clientes where Correo =@Correo and Passwd=@Passwd
end
go
if OBJECT_ID('AdministraArticulo','P')is not null
drop Procedure AdministraArticulo
go
Create Procedure AdministraArticulo
(@Codigo varchar(20)=null,@Descripcion varchar(50)=null,@Precio decimal(12,4)=null,@Stock int=null,@Accion varchar(1))
as
IF @Accion='A'
begin
	if NOT EXISTS(SELECT 1 FROM Articulos WHERE CODIGO=@Codigo)
	begin
		insert into Articulos (Codigo,Descripcion,Precio,Stock)
		values (@Codigo,@Descripcion,@Precio,@Stock)
		select @@ROWCOUNT
	end
	else
	begin
		update Articulos set Codigo=@Codigo,Descripcion=@Descripcion,Precio=@Precio,Stock=@Stock where Codigo =@Codigo
		select @@ROWCOUNT
	end

End
IF @Accion='B'
begin
		delete Articulos where Codigo=@Codigo
		select @@ROWCOUNT
End
IF @Accion='C'
begin
		Select Codigo,Descripcion,Precio,Stock from Articulos where Codigo =@Codigo
End
IF @Accion='T'
begin
		Select Codigo,Descripcion,Precio,Stock from Articulos
End
go
if OBJECT_ID('InventarioTienda','P')is not null
drop Procedure InventarioTienda
go
Create Procedure InventarioTienda (@Codigo varchar(20)=null,@IdTienda int=null)
as

	  If exists(select 1 from ArticulosXTienda where IdTienda=@IdTienda and Codigo=@Codigo)
	  begin
			delete from ArticulosXTienda where IdTienda=@IdTienda and Codigo=@Codigo
	  end
		insert into ArticulosXTienda (Codigo,IdTienda)
		values (@Codigo,@IdTienda)
		select @@ROWCOUNT
go
if OBJECT_ID('RegistraVentas','P')is not null
drop Procedure RegistraVentas
go
Create Procedure RegistraVentas (@IdTienda int=null,@IdCliente int=null,@Codigo varchar(20)=null,@Precio decimal(12,4)=null,@Cantidad int)
as

insert into Ventas(IdTienda,IdCliente,Codigo,Precio,Cantidad,FechaVenta)
values (@IdTienda,@IdCliente,@Codigo,@Precio,@Cantidad,GETDATE())

update Articulos set Stock=stock - @Cantidad where  Codigo=@Codigo