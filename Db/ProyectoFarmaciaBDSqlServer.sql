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
	telefono_farmacia varchar(90) not null,
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


insert into tb_farmacia values ('FR00000001','R1379477412133','Farmacias TuSalud','990230423','Las palmeras - Pachacamac',1)
go