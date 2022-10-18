use master
go

drop database if exists CyberLab
go
-- creamos la bd
create database CyberLab
go
-- activamos la bd
use CyberLab
go


if OBJECT_ID('tb_farmacia','U') is null create table tb_farmacia
(
	id_farmacia char(10) primary key,
	ruc_farmacia char(14) not null unique,
	razonsocial_farmacia varchar(90) not null,
	telefono_farmacia varchar(15) not null,
    direccion_farmacia varchar(90) not null, /*tabla direccion pendiente*/
	estado bit not null
)
go


create or alter procedure usp_farmacias_listar
as
begin
	select * from tb_farmacia
end
go

create or alter procedure usp_farmacia_agregar
@id char(10),
@ruc char(14),
@razonsocial varchar(90),
@telefono varchar(15),
@direccion varchar(90)
as
begin
	insert into tb_farmacia values (@id,@ruc,@razonsocial,@telefono,@direccion,1)
end
go



create or alter procedure usp_farmacia_actualizar
@id char(10),
@ruc char(14),
@razonsocial varchar(90),
@telefono varchar(15),
@direccion varchar(90)
as
begin
	update tb_farmacia set ruc_farmacia = @ruc, razonsocial_farmacia = @razonsocial, telefono_farmacia = @telefono, direccion_farmacia = @direccion where id_farmacia = @id
end
go

create or alter procedure usp_farmacia_eliminar
@id char(10)
as
begin
	update tb_farmacia set estado = 0 where id_farmacia = @id
end
go




/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/








/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/


if OBJECT_ID('tb_tipoproducto','U') is null create table tb_tipoproducto
(
    id_tipoprod int identity(1,1) primary key,
    nombre_tipoprod varchar(60) unique  not null,
    estado bit not null
)
go




create or alter procedure usp_tipoproducto_listar
as
begin
	select * from tb_tipoproducto
end
go


create or alter procedure usp_tipoproducto_agregar
@nombre char(14)
as
begin
	insert into tb_tipoproducto (nombre_tipoprod,estado) values (@nombre,1)
end
go


create or alter procedure usp_tipoproducto_actualizar
@id int,
@nombre char(14)
as
begin
	update tb_tipoproducto set nombre_tipoprod=@nombre  where id_tipoprod = @id
end
go


create or alter procedure usp_tipoproducto_eliminar
@id int
as
begin
	update tb_tipoproducto set estado = 0 where id_tipoprod = @id
end
go



insert into tb_tipoproducto (nombre_tipoprod,estado) values ('Analgésico',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('Antiinflamatorio',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('Antihístaminico',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('Antialérgicos',1)
go




/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/


if OBJECT_ID('tb_laboratorio','U') is null  create table tb_laboratorio
(
	id_laboratorio char(10) primary key,
	ruc_laboratorio char(11) not null unique,
	razonsocial_laboratorio varchar(90) not null,
	telefono_laboratorio varchar(15) not null,
	estado bit not null
)
go



create or alter procedure usp_laboratorio_listar
as
begin
	select * from tb_laboratorio
end
go


create or alter procedure usp_laboratorio_agregar
@id char(10),
@ruc char(11),
@razonsocial varchar(90),
@telefono varchar(15)
as
begin
	insert into tb_laboratorio values (@id,@ruc,@razonsocial,@telefono,1)
end
go


create or alter procedure usp_laboratorio_actualizar
@id char(10),
@ruc char(11),
@razonsocial varchar(90),
@telefono varchar(15)
as
begin
	update tb_laboratorio set ruc_laboratorio = @ruc, razonsocial_laboratorio = @razonsocial, telefono_laboratorio = @telefono   where id_laboratorio = @id
end
go


create or alter procedure usp_laboratorio_eliminar
@id char(10)
as
begin
	update tb_laboratorio set estado = 0 where id_laboratorio = @id
end
go



/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/



if OBJECT_ID('tb_producto','U') is null  create table tb_producto
(
	id_producto char(10) primary key,
    id_laboratorio char(10) not null,
    id_tipoprod int not null,
	codigobar_producto varchar(14) not null unique,
	descripcion_producto varchar(240) not null,
	marca_producto varchar(12) not null,
	estado bit not null,
    foreign key(id_tipoprod) references tb_tipoproducto(id_tipoprod),
    foreign key(id_laboratorio) references tb_laboratorio(id_laboratorio)
)
go

 
create or alter procedure usp_producto_listar
as
begin
	select * from tb_producto
end
go


create or alter procedure usp_producto_agregar
@id char(10),
@idlaboratorio char(10),
@idtipo int,
@codigobarras varchar(14),
@descripcion varchar(240),
@marca varchar(12)
as
begin
	insert into tb_producto values (@id,@idlaboratorio,@idtipo,@codigobarras,@descripcion,@marca,1)
end
go


create or alter procedure usp_producto_actualizar
@id char(10),
@idlaboratorio char(10),
@idtipo int,
@codigobarras varchar(14),
@descripcion varchar(240),
@marca varchar(12)
as
begin
	update tb_producto set id_laboratorio = id_laboratorio, id_tipoprod = @idtipo, codigobar_producto = @codigobarras, descripcion_producto = @descripcion, marca_producto = @marca   where id_producto = @id
end
go


create or alter procedure usp_producto_eliminar
@id char(10)
as
begin
	update tb_producto set estado = 0 where id_producto = @id
end
go



/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/


if OBJECT_ID('tb_facturacion','U') is null create table tb_facturacion
(
	id_facturacion char(10) not null primary key,
	id_farmacia char(10) not null,
	fechaemision_facturacion date not null,
	subtotal_facturacion float not null,
	estado bit not null,
	foreign key(id_farmacia) references tb_farmacia(id_farmacia)
)
go


 
create or alter procedure usp_facturacion_listar
as
begin
	select * from tb_facturacion
end
go


create or alter procedure usp_facturacion_agregar
@id char(10),
@idfarmacia char(10),
@fechaemision date,
@subtotal float
as
begin
	insert into tb_facturacion values (@id,@idfarmacia,@fechaemision,@subtotal,1)
end
go


create or alter procedure usp_facturacion_actualizar
@id char(10),
@idfarmacia char(10),
@fechaemision date,
@subtotal float
as
begin
	update tb_facturacion set id_farmacia = @idfarmacia, fechaemision_facturacion = @fechaemision, subtotal_facturacion = @subtotal  where id_facturacion = @id
end
go


create or alter procedure usp_facturacion_eliminar
@id char(10)
as
begin
	update tb_facturacion set estado = 0 where id_facturacion = @id
end
go



/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/



if OBJECT_ID('tb_detalle_facturacion','U') is null create table tb_detalle_facturacion
(
	id_facturacion char(10) not null,
	id_producto char(10) not null,
	cantidadproducto_detalle_facturacion int not null,
	monto_detalle_facturacion float not null,
	estado bit not null,
	primary key(id_producto,id_facturacion),
	foreign key(id_producto) references tb_producto(id_producto),
	foreign key(id_facturacion) references tb_facturacion(id_facturacion)
)
go


create or alter procedure usp_detalle_facturacion_listar
as
begin
	select * from tb_detalle_facturacion
end
go


create or alter procedure usp_facturacion_farmacia_producto_listar
as
begin
	select p.descripcion_producto,f.fechaemision_facturacion,fr.ruc_farmacia,fr.razonsocial_farmacia,f.subtotal_facturacion from tb_detalle_facturacion df inner join tb_facturacion f on df.id_facturacion = f.id_facturacion join tb_producto p on df.id_producto = p.id_producto join tb_farmacia fr on f.id_farmacia = fr.id_farmacia
end
go



create or alter procedure usp_detalle_facturacion_agregar
@idfacturacion char(10),
@idproducto char(10),
@cantidadproducto char(14),
@monto float
as
begin
	insert into tb_detalle_facturacion values (@idfacturacion,@idproducto,@cantidadproducto,@monto,1)
end
go



create or alter procedure usp_detalle_facturacion_actualizar
@idfacturacion char(10),
@idproducto char(10),
@cantidadproducto char(14),
@monto float
as
begin
	update tb_detalle_facturacion set cantidadproducto_detalle_facturacion = @cantidadproducto, monto_detalle_facturacion = @monto where id_facturacion = @idfacturacion and id_producto = @idproducto
end
go


create or alter procedure usp_detalle_facturacion_eliminar
@idfacturacion char(10),
@idproducto char(10)
as
begin
	update tb_detalle_facturacion set estado = 0 where id_facturacion = @idfacturacion and id_producto = @idproducto
end
go




/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/
/*--------------------------------------------------------------------------------*/















insert into tb_farmacia values ('FR00000001','R1379477412133','Farmacias TuSalud','990230423','Las palmeras - Pachacamac',1)
go

insert into tb_farmacia values ('FR00000002','R1379477412111','Farmacias NN','993330423','Los alamos - Lurin',0)
go

insert into tb_farmacia values ('FR00000003','R1379477412000','Farmacias Rimac','991111423','Gardenias - Surco',1)
go