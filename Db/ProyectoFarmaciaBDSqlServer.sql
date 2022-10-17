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


insert into tb_farmacia values ('FR00000001','R1379477412133','Farmacias TuSalud','990230423','Las palmeras - Pachacamac',1)
go

insert into tb_farmacia values ('FR00000002','R1379477412111','Farmacias NN','993330423','Los alamos - Lurin',0)
go

insert into tb_farmacia values ('FR00000003','R1379477412000','Farmacias Rimac','991111423','Gardenias - Surco',1)
go