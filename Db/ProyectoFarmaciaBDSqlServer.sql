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

insert into tb_farmacia values ('FR00000001','R1379477412133','Farmacias TuSalud','990230423','Las palmeras - Pachacamac',1)
go
insert into tb_farmacia values ('FR00000002','R1379477412111','Farmacias NN','993330423','Los alamos - Lurin',0)
go
insert into tb_farmacia values ('FR00000003','R1379477412000','Farmacias Rimac','991111423','Gardenias - Surco',1)
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_farmacias_listar
as
begin
	select * from tb_farmacia
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_farmacia_agregar
@id char(10),
@ruc char(14),
@razonsocial varchar(90),
@telefono varchar(15),
@direccion varchar(90)
as
begin
	insert into tb_farmacia values (@id,@ruc,@razonsocial,@telefono,@direccion,0)
end
go
/*--------------------------------------------------------------------------------*/

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
/*--------------------------------------------------------------------------------*/

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


if OBJECT_ID('tb_usuario_farmacia','U') is null create table tb_usuario_farmacia
(
	id_usuario_farmacia char(10) primary key,
	id_farmacia char(10) not null,
	alias_farmacia varchar(90) not null,
	contrasena_farmacia varchar(150) not null,
	estado bit not null,
	foreign key(id_farmacia) references tb_farmacia(id_farmacia)
)
go

insert into tb_usuario_farmacia values ('UF00000001','FR00000001','tusaludf','tusalud99',1)
go
insert into tb_usuario_farmacia values ('UF00000002','FR00000002','nnf','nnf99',0)
go
insert into tb_usuario_farmacia values ('UF00000003','FR00000003','rimacf','rimac99',1)
go
/*--------------------------------------------------------------------------------*/


create or alter function dbo.autogenerausuariofarmacia() returns varchar(10)
As
Begin
	Declare @n int
	Declare @cod varchar(10)=(Select top 1 id_usuario_farmacia from tb_usuario_farmacia order by 1 desc)

	if(@cod is null)
		Set @n=1
	else
		Set @n=CAST(subString(@cod,3,10) as int)+1

	return CONCAT('UF',REPLICATE('0',8-LEN(@n)),@n)
End
go
/*--------------------------------------------------------------------------------*/




create or alter procedure usp_usuarios_farmacia_listar
as
begin
	select * from tb_usuario_farmacia
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_usuario_farmacia_agregar
@idfarmacia char(10),
@alias varchar(90),
@contrasena varchar(150)
as
begin
	declare @codigo char(10)
	set @codigo=dbo.autogenerausuariofarmacia()
	insert into tb_usuario_farmacia values (@codigo,@idfarmacia,@alias,@contrasena,0)
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_usuario_farmacia_actualizar
@id char(10),
@alias varchar(90),
@contrasena varchar(150)
as
begin
	update tb_usuario_farmacia set alias_farmacia = @alias, contrasena_farmacia = @contrasena where id_usuario_farmacia = @id
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_usuario_farmacia_eliminar
@id char(10)
as
begin
	update tb_usuario_farmacia set estado = 0 where id_usuario_farmacia = @id
end
go



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
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_tipoproducto_agregar
@nombre char(60)
as
begin
	insert into tb_tipoproducto (nombre_tipoprod,estado) values (@nombre,1)
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_tipoproducto_actualizar
@id int,
@nombre char(14)
as
begin
	update tb_tipoproducto set nombre_tipoprod=@nombre  where id_tipoprod = @id
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_tipoproducto_eliminar
@id int
as
begin
	update tb_tipoproducto set estado = 0 where id_tipoprod = @id
end
go

insert into tb_tipoproducto (nombre_tipoprod,estado) values ('ACETILCISTEINA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('ACIDO ACETILSALICILICO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('AMBROXOL',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('AMOXICILINA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('BISMUTO SUBSALICILATO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('CETIRIZINA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('CIANOCOBALAMINA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('CIPROFLOXACINO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('CLORFENAMINA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('CLOTRIMAZOL',1)

insert into tb_tipoproducto (nombre_tipoprod,estado) values ('DEXAMETASONA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('DICLOFENACO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('FLUCONAZOL',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('IBUPROFENO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('NAFAZOLINA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('NAPROXENO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('PARACETAMOL',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('SILDENAFILO',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('SIMETICONA',1)
insert into tb_tipoproducto (nombre_tipoprod,estado) values ('SUCRALFATO',1)


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




insert into tb_laboratorio values ('LAB0000001','R1379477414','LaboratoriosPortugal','990230999',1)
go

go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_laboratorio_listar
as
begin
	select * from tb_laboratorio
end
go
/*--------------------------------------------------------------------------------*/

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
/*--------------------------------------------------------------------------------*/

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
/*--------------------------------------------------------------------------------*/

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
	stock_producto int not null,
	preciounid_producto float not null,
	imagen_producto varchar (50) null, -- -----------------------------------------
	estado bit not null,
    foreign key(id_tipoprod) references tb_tipoproducto(id_tipoprod),
    foreign key(id_laboratorio) references tb_laboratorio(id_laboratorio)
)
go


/*--------------------------------------------------------------------------------*/


create or alter function dbo.autogeneraproducto() returns varchar(10)
As
Begin
	Declare @n int
	Declare @cod varchar(10)=(Select top 1 id_producto from tb_producto order by 1 desc)

	if(@cod is null)
		Set @n=1
	else
		Set @n=CAST(subString(@cod,5,10) as int)+1

	return CONCAT('PROD',REPLICATE('0',6-LEN(@n)),@n)
End
go
/*--------------------------------------------------------------------------------*/

print dbo.autogeneraproducto()

select * from tb_producto


insert into tb_producto values ('PROD000001','LAB0000001',1,'EN07639','MUCOCETIL 100 mg','ROXFARMA',500,20.00,'1.jpg',1)
insert into tb_producto values ('PROD000002','LAB0000001',1,'NG4822','MUCOASMAT 200 mg','VITA PHARMA',200,69.00,'2.jpg',1)
insert into tb_producto values ('PROD000003','LAB0000001',1,'EE07780','MUXATIL 300 300 mg','INTI PERU',600,75.00,'3.jpg',1)
insert into tb_producto values ('PROD000004','LAB0000001',1,'EE08148','BRIMODIN 600  mg ','IQ FARMA',600,70.00,'4.jpg',1)
insert into tb_producto values ('PROD000005','LAB0000001',1,'EN05818','MUCOASMAT 600 mg','VITA PHARMA',700,105.00,'5.jpg',1)

insert into tb_producto values ('PROD000006','LAB0000001',2,'EN07311','ASPIRINA 100 mg ','BAYER',900,70.00,'6.jpg',1)
insert into tb_producto values ('PROD000007','LAB0000001',2,'EN05416','MIGRALIVIA 250 mg','MEDROCK',400,148.00,'7.jpg',1)
insert into tb_producto values ('PROD000008','LAB0000001',2,'EN04329','ACIDO ACETILSALICILICO 100  mg ','LABOT',300,30.00,'8.jpg',1)
insert into tb_producto values ('PROD000009','LAB0000001',2,'EN04490','EVERDRIN FORTE 250  mg','QUILLAPHARMA',500,148.00,'9.jpg',1)
insert into tb_producto values ('PROD000010','LAB0000001',2,'EE04419','ASPIRINA ULTRA  500 mg','BAYER',200,128.00,'10.jpg',1)

insert into tb_producto values ('PROD000011','LAB0000001',3,'EE07885','BIOBRONCOL 500  mg ','IQ FARMA',300,125.00,'11.jpg',1)
insert into tb_producto values ('PROD000012','LAB0000001',3,'EE03376','MUCOTRIM DILAT 7.5  mg','ROXFARMA',500,18.00,'12.jpg',1)
insert into tb_producto values ('PROD000013','LAB0000001',3,'EE06838','MUCOTRIM PLUS 2  mg ','ROXFARMA',600,18.00,'13.jpg',1)
insert into tb_producto values ('PROD000014','LAB0000001',3,'EN07179','AMBROXOL 30  mg','ROXFARMA',600,5.00,'14.jpg',1)
insert into tb_producto values ('PROD000015','LAB0000001',3,'EN06089','CLARIBRONCOL F 500 mg','QUILLAPHARMA',600,135.00,'15.jpg',1)
insert into tb_producto values ('PROD000016','LAB0000001',3,'EE07596','AMBROXOL 15  mg','QUILLAPHARMA',600,5.00,'16.jpg',1)
insert into tb_producto values ('PROD000017','LAB0000001',3,'EE08066','AMBROXOL CLORHIDRATO 250  mg ','NAT. y GEN.',600,10.00,'17.jpg',1)

insert into tb_producto values ('PROD000018','LAB0000001',4,'EE05512','ACIDO CLAVULANICO 250  mg','NATURGEN',500,18.00 ,'18.jpg',1)
insert into tb_producto values ('PROD000019','LAB0000001',4,'EE07948','AMOXICILINA 250  mg','NATURGEN',800,5.00 ,'19.jpg',1)
insert into tb_producto values ('PROD000020','LAB0000001',4,'EE08194','AMOXICILINA 500  mg','FARMIN',800,38.00 ,'20.jpg',1)
insert into tb_producto values ('PROD000021','LAB0000001',4,'EN07295','AMOXICILINA 500  mg Tableta Recubierta 100 unid.','IQ FARMA',800,38.00 ,'21.jpg',1)
insert into tb_producto values ('PROD000022','LAB0000001',4,'EN07066','AMOXIPHARM CL 25 0 mg ','D-OLAPHARM',500,22.00 ,'22.jpg',1)
insert into tb_producto values ('PROD000023','LAB0000001',4,'NG6412','NADIMOX 500 mg ','QUILLAPHARMA',800,150.00 ,'23.jpg',1)
insert into tb_producto values ('PROD000024','LAB0000001',4,'EN05486','DUO-BRONCOL 250  mg ','RUCEF',900,18.00 ,'24.jpg',1)

insert into tb_producto values ('PROD000025','LAB0000001',5,'EN03171','BISMUCAR SABOR A CEREZA 262  mg','EUROFARMA',300,100.00 ,'25.jpg',1)
insert into tb_producto values ('PROD000026','LAB0000001',5,'EE08190','BISMUTOL 87.33  mg/5 mL','TEVA',300,16.50 ,'26.jpg',1)
insert into tb_producto values ('PROD000027','LAB0000001',5,'EE06741','BISMUTOL 87.33  mg','TEVA',400,23.00 ,'27.jpg',1)
insert into tb_producto values ('PROD000028','LAB0000001',5,'EN07635','BISMUALIV 262.5  mg','NAT. y GEN.',400,80.00 ,'28.jpg',1)
insert into tb_producto values ('PROD000029','LAB0000001',5,'EN06573','BISMUTOL 262 mg','TEVA',400,110.00 ,'29.jpg',1)
-- 29
insert into tb_producto values ('PROD000030','LAB0000001',6,'EE05332 ','ALEXMIN 5  mg','GENCO.',400,15.00 ,'30.jpg',1)
insert into tb_producto values ('PROD000031','LAB0000001',6,'EN06168','CETIRIZINA 5  mg','GENCO',500,5.00 ,'31.jpg',1)
insert into tb_producto values ('PROD000032','LAB0000001',6,'EE04689','TDN ALLERGY 10  mg ','SHERFARMA',700,148.00 ,'32.jpg',1)
insert into tb_producto values ('PROD000033','LAB0000001',6,'EE05639','CETIRIZINA  10  mg','FARMIN',800,30.80 ,'33.jpg',1)
insert into tb_producto values ('PROD000034','LAB0000001',6,'BE01110','XENILER 10  mg','ROXFARMA',800,19.80 ,'34.jpg',1)
insert into tb_producto values ('PROD000035','LAB0000001',6,'EN06728','CETIRIZINA 10  mg ','NATURGEN',700,18.00 ,'35.jpg',1)

insert into tb_producto values ('PROD000036','LAB0000001',7,'EN07683','ANEURIN 1000 100  mg','ALBIS',700,26.00,'36.jpg',1)
insert into tb_producto values ('PROD000037','LAB0000001',7,'EN05880','DEXA BENALGIN  Solución Inyectable 2 unid.','LUSA',500,29.00,'37.jpg',1)
insert into tb_producto values ('PROD000038','LAB0000001',7,'EN01321','DOLO NEUROPRESS FORTE','PHARMED',700,120.00,'38.jpg',1)
insert into tb_producto values ('PROD000039','LAB0000001',7,'EN05972','VITAVERAN B12 CIP','MARKOS',600,28.00,'39.jpg',1)
insert into tb_producto values ('PROD000040','LAB0000001',7,'EE07903','DOLO BENALGIN','LUSA',600,28.00,'40.jpg',1)
insert into tb_producto values ('PROD000041','LAB0000001',7,'EE03843','BENALGIN 1000','LUSA',500,18.50,'41.jpg',1)
insert into tb_producto values ('PROD000042','LAB0000001',7,'EE01694','DOLO NEUROBION FORTE','MERCK',700,500.00,'42.jpg',1)
insert into tb_producto values ('PROD000043','LAB0000001',7,'EE01724','BENALGIN 10000','LUSA',900,19.80,'43.jpg',1)
-- 43
insert into tb_producto values ('PROD000044','LAB0000001',8,'EN06975','CIPROLIP 500 mg','LIPHARMA',200,200.00,'44.jpg',1)
insert into tb_producto values ('PROD000045','LAB0000001',8,'EE04012','UROPOL FORTE','HERSIL',500,250.00,'45.jpg',1)
insert into tb_producto values ('PROD000046','LAB0000001',8,'EE04011','CIPROFLOXACINO 0.3%','INVERS',200,20.00,'46.jpg',1)
insert into tb_producto values ('PROD000047','LAB0000001',8,'EE08165','CIPROFLOXACINO 500  mg','NAT. y GEN',200,37.00,'47.jpg',1)
insert into tb_producto values ('PROD000048','LAB0000001',8,'E11206','ONICAX 750 750  mg','GABBLAN',300,1350.00,'48.jpg',1)
insert into tb_producto values ('PROD000049','LAB0000001',8,'EE04743','CIPROPHARMA500  mg','INTIPHARMA',400,1998.00,'49.jpg',1)

insert into tb_producto values ('PROD000050','LAB0000001',9,'EE04741','GRIPTOTAL','TEVA',400,90.00,'50.jpg',1)
insert into tb_producto values ('PROD000051','LAB0000001',9,'N21715','CLORFENAMINA MALEATO 10  mg','LUSA',400,50.00,'51.jpg',1)
insert into tb_producto values ('PROD000052','LAB0000001',9,'EE01948','NASTIZOL COMPOSITUM JUNIOR NF ANTIGRIPAL 80  mg','BAGO',500,134.00,'52.jpg',1)
insert into tb_producto values ('PROD000053','LAB0000001',9,'EE01998','CHAO  Tableta Recubierta','GENOMMA',500,138.00,'53.jpg',1)
insert into tb_producto values ('PROD000054','LAB0000001',9,'EE05109','CLORFENAMINA MALEATO 4 mg','NAT. y GEN.',600,27.00,'54.jpg',1)
insert into tb_producto values ('PROD000055','LAB0000001',9,'N20371','GRIFANTIL','UNIDASPERU',600,25.00,'55.jpg',1)
insert into tb_producto values ('PROD000056','LAB0000001',9,'N20373','GRIPA C JUNIOR','MEDIFARMA',500,95.00,'56.jpg',1)
insert into tb_producto values ('PROD000057','LAB0000001',9,'N20377','DEXABRON NUEVA FORMULA','MARKOS',500,200.00,'57.jpg',1)
insert into tb_producto values ('PROD000058','LAB0000001',9,'N20372','NASTIFLU  Tableta 60 unid','TEVA',600,107.00,'58.jpg',1)
insert into tb_producto values ('PROD000059','LAB0000001',9,'N26940','FRENAGRIP 500 mg','UNIDASPERU',500,40.00,'59.jpg',1)
insert into tb_producto values ('PROD000060','LAB0000001',9,'EN07662','TAPSIN PLUS CALIENTE NOCHE 650  mg','MAVER',500,118.00,'60.jpg',1)
insert into tb_producto values ('PROD000061','LAB0000001',9,'EN07600','CLORFENAMINA MALEATO 2  mg','LABOT',600,5.00,'61.jpg',1)
insert into tb_producto values ('PROD000062','LAB0000001',9,'EN02513','CLORFENAMINA 10 mg','LABOT',700,200.00,'62.jpg',1)
insert into tb_producto values ('PROD000063','LAB0000001',9,'EN02512','DR. FLU  Cápsula Blanda 100 unid','DROPESAC',500,149.00,'63.jpg',1)
insert into tb_producto values ('PROD000064','LAB0000001',9,'EN02511','DAYFLU  Cápsula Blanda 100 unid.','UNIMED',500,148.00,'64.jpg',1)
insert into tb_producto values ('PROD000065','LAB0000001',9,'RN03118','GRIPAMEDIC C  Tableta Recubierta 100 unid. ','FARMMEDICAL',500,145.00,'65.jpg',1)
-- 65
insert into tb_producto values ('PROD000066','LAB0000001',10,'EN06233','MICO DERMASAN 1','SHERFARMA',800,6.50,'66.jpg',1)
insert into tb_producto values ('PROD000067','LAB0000001',10,'EE01598','PORTIL  Crema 1 unid.','GENOMMA',500,8.50,'67.jpg',1)
insert into tb_producto values ('PROD000068','LAB0000001',10,'EE01528','CLOTRIMAZOL 1','GENOMMA',500,4.00,'68.jpg',1)
insert into tb_producto values ('PROD000069','LAB0000001',10,'EN00787','LOMECAN V 2','GENOMMA',800,36.50,'69.jpg',1)
insert into tb_producto values ('PROD000070','LAB0000001',10,'EE01414','CLOTRIMAZOL 1','GENOMMA',500,3.50,'70.jpg',1)

insert into tb_producto values ('PROD000071','LAB0000001',11,'EE01914','DEXAMETASONA 4  mg','panadol',900,62.50,'71.jpg',1)
insert into tb_producto values ('PROD000072','LAB0000001',11,'EE02451','MEDICORT 4  mg','panadol',900,8.50,'72.jpg',1)
insert into tb_producto values ('PROD000073','LAB0000001',11,'N25382','DEXALOR  Tableta Recubierta 100 unid.','panadol',500,200.00,'73.jpg',1)
insert into tb_producto values ('PROD000074','LAB0000001',11,'EN07674','DEXACORT 4  mg','panadol',500,9.90,'74.jpg',1)
insert into tb_producto values ('PROD000075','LAB0000001',11,'EN07660','DEXCORTIL 4 mg','panadol',500,45.00,'75.jpg',1)
insert into tb_producto values ('PROD000076','LAB0000001',11,'N25384','FRAMIDEX NF 1','panadol',900,13.00,'76.jpg',1)

-- 76-----------------------------------------------------------------------------------------------------


insert into tb_producto values ('PROD000077','LAB0000001',12,'EE08057','UROTAN-D 50  mg','MARKOS',700,200.00,'77.jpg',1)
insert into tb_producto values ('PROD000078','LAB0000001',12,'EE01576','DOLODRAN EXTRA FORTE 500  mg','NAT y GEN.',900,100.00,'78.jpg',1)
insert into tb_producto values ('PROD000079','LAB0000001',12,'EE02590','MIOPRESS FORTE 500  mg','PHARMED',700,200.00,'79.jpg',1)
insert into tb_producto values ('PROD000080','LAB0000001',12,'EN06132','MIODEL RELAX 500 mg','DELFARMA',600,200.00,'80.jpg',1)
insert into tb_producto values ('PROD000081','LAB0000001',12,'RN06133','MIOFEDROL RELAX 75 mg','MARKOS',600,15.00,'81.jpg',1)
insert into tb_producto values ('PROD000082','LAB0000001',12,'EE01892','DOLOPRESS RAPID 300 mg','PHARMED',800,195.00,'82.jpg',1)
insert into tb_producto values ('PROD000083','LAB0000001',12,'EN02685','DICLOFENACO SODICO 75 mg','JPS',800,248.00,'83.jpg',1)
insert into tb_producto values ('PROD000084','LAB0000001',12,'EN07130','FISIODOL FORTE 2','IQ FARMA',400,22.00,'84.jpg',1)
insert into tb_producto values ('PROD000085','LAB0000001',12,'EN03404','DICLOFENACO 50  mg','IQ FARMA',400,28.00,'85.jpg',1)
insert into tb_producto values ('PROD000086','LAB0000001',12,'EE05333','DIOXAFLEX CB PLUS 50  mg','BAGO',400,456.00,'86.jpg',1)
insert into tb_producto values ('PROD000087','LAB0000001',12,'R22818','ANAFLEX MUJER 25  mg','BAGO',300,36.00,'87.jpg',1)
insert into tb_producto values ('PROD000088','LAB0000001',12,'R22819','DICLOFENACO 1','BAGO',300,7.50,'88.jpg',1)
-- 88-----------------------------------------------------------------------------------------------------------
insert into tb_producto values ('PROD000089','LAB0000001',13,'E22820','FLUMIL 150  mg','EUROFARMA',800,8.00,'89.jpg',1)
insert into tb_producto values ('PROD000090','LAB0000001',13,'EE05306','FLUCONAZOL 150  mg','INDUSTRIA',800,1.80,'90.jpg',1)
insert into tb_producto values ('PROD000091','LAB0000001',13,'EE05301','FLUCONAZINN 150  mg','DRONNVELS',700,350.00,'91.jpg',1)
insert into tb_producto values ('PROD000092','LAB0000001',13,'E22587','FLUCONAZOL 150  mg','NAT y GEN',900,98.00,'92.jpg',1)
insert into tb_producto values ('PROD000093','LAB0000001',13,'EE04417','PLUSZOL 150  mg','MEDICAL',700,6.00,'93.jpg',1)

insert into tb_producto values ('PROD000094','LAB0000001',14,'EE03710','IBUPROFENO 800  mg','NAT y GEN',900,58.00,'94.jpg',1)
insert into tb_producto values ('PROD000095','LAB0000001',14,'EE04931','IBUPROFENO 100  mg','HERSIL',600,4.50,'95.jpg',1)
insert into tb_producto values ('PROD000096','LAB0000001',14,'E22661','DOLORAL  100  mg','HERSIL',600,11.80,'96.jpg',1)
insert into tb_producto values ('PROD000097','LAB0000001',14,'E22439','DOLOFLAM EXTRAFORTE 400  mg','JOHNSON',700,128.00,'97.jpg',1)
insert into tb_producto values ('PROD000098','LAB0000001',14,'E22438','DOLOKID S 100 mg','INTIPHARMA',900,11.00,'98.jpg',1)
insert into tb_producto values ('PROD000099','LAB0000001',14,'EE02739','DOLONET FORTE 400  mg','UNIMED',700,118.00,'99.jpg',1)
insert into tb_producto values ('PROD000100','LAB0000001',14,'EE02740','IBUPROFENO 400  mg','NAT y GEN',800,28.00,'100.jpg',1)
-- 100
insert into tb_producto values ('PROD000101','LAB0000001',15,'E22444','FLORIL NUEVA FORMULA 0.03','LANSIER',900,12.50,'101.jpg',1)
insert into tb_producto values ('PROD000102','LAB0000001',15,'E22443','FLORIL NUEVA FORMULA 0.05','LANSIER',700,10.50,'102.jpg',1)
insert into tb_producto values ('PROD000103','LAB0000001',15,'EE02742','RINOFLU  0.1','INTRADEVCO',700,14.50,'103.jpg',1)

insert into tb_producto values ('PROD000104','LAB0000001',16,'EN05928','APRONAX 550 mg','BAYER',800,240.00,'104.jpg',1)
insert into tb_producto values ('PROD000105','LAB0000001',16,'EN01577','NAPROXENO SODICO 550 mg','PHARMED',800,38.00,'105.jpg',1)
insert into tb_producto values ('PROD000106','LAB0000001',16,'EN05710','PONSTAN RD 220  mg','PFIZER',700,118.00,'106.jpg',1)
insert into tb_producto values ('PROD000107','LAB0000001',16,'NG3379','NAPROCOP COMPUESTO 275 mg','PHARMED',900,200.00,'107.jpg',1)
insert into tb_producto values ('PROD000108','LAB0000001',16,'EN05055','NOPRAMAX 550  mg','ROXFARMA',700,150.00,'108.jpg',1)
insert into tb_producto values ('PROD000109','LAB0000001',16,'EN01633','FEMYNASS RD 275 mg','DRONNVELS',800,150.00,'109.jpg',1)
insert into tb_producto values ('PROD000110','LAB0000001',16,'EN06746','FEBRADOL EXTRA FORTE  275 mg','NOVAX',900,200.00,'110.jpg',1)
-- 110
insert into tb_producto values ('PROD000111','LAB0000001',17,'EN07233','PARACETAMOL 120  mg','GLAXO',600,5.00,'111.jpg',1)
insert into tb_producto values ('PROD000112','LAB0000001',17,'EN03433','PANADOL 80  mg','GLAXO',700,80.00,'112.jpg',1)
insert into tb_producto values ('PROD000113','LAB0000001',17,'EN07239','PANADOL 500  mg','GSK',500,59.00,'113.jpg',1)
insert into tb_producto values ('PROD000114','LAB0000001',17,'EN02433','PANADOL PARA NIÑOS LIQUIDO 160  mg','GSK',900,18.00,'114.jpg',1)
insert into tb_producto values ('PROD000115','LAB0000001',17,'ER92973','Comprimido 500 mg','GSK',500,30.00,'115.jpg',1)
insert into tb_producto values ('PROD000116','LAB0000001',17,'EN00305','PARACETAMOL 100 mg','UNIDASPERU',900,3.00,'116.jpg',1)
insert into tb_producto values ('PROD000117','LAB0000001',17,'EN00366','ALICAM FORTE 500 mg','INRETAIL',600,48.00,'117.jpg',1)
insert into tb_producto values ('PROD000118','LAB0000001',17,'EE03153','GESIDOL® 500 mg','FARVET',700,40.00,'118.jpg',1)
insert into tb_producto values ('PROD000119','LAB0000001',17,'EN57578','PARACETAMOL 500  mg','FARVET',800,24.00,'119.jpg',1)
insert into tb_producto values ('PROD000120','LAB0000001',17,'N22774','MAFIDOL 120  mg','GLAXO',900,72.00,'120.jpg',1)
insert into tb_producto values ('PROD000121','LAB0000001',17,'EE03123','PANADOL EFERVESCENTE 500  mg','GLAXO',800,36.00,'121.jpg',1)
insert into tb_producto values ('PROD000122','LAB0000001',17,'E22479','TAPSIN INFANTIL 100  mg','MAVER',800,12.00,'122.jpg',1)

insert into tb_producto values ('PROD000123','LAB0000001',18,'EE06167','SILDENAFILO 50  mg','TEVA',800,3.00,'123.jpg',1)
insert into tb_producto values ('PROD000124','LAB0000001',18,'EE07610','SILDEX 100 100  mg','PHARMED',800,7.00,'124.jpg',1)
insert into tb_producto values ('PROD000125','LAB0000001',18,'EE02508','SILDEX 50 mg','PHARMED',900,6.00,'125.jpg',1)
insert into tb_producto values ('PROD000126','LAB0000001',18,'EE06056','VIALZA 50 mg','QUILLA',700,60.00,'126.jpg',1)
insert into tb_producto values ('PROD000127','LAB0000001',18,'EE06168','SILDENAFILO 100  mg','TEVA',700,4.00,'127.jpg',1)
insert into tb_producto values ('PROD000128','LAB0000001',18,'EE02598','ELEVAMAX 100 100  mg','INTIPHARMA',600,200.00,'128.jpg',1)


insert into tb_producto values ('PROD000129','LAB0000001',19,'EN05216','GASEOMEDIC 80  mg','MEDICAL',900,15.00,'129.jpg',1)
insert into tb_producto values ('PROD000130','LAB0000001',19,'EN00023','SIMETICONA 80  mg','INDUSTRIA',800,18.00,'130.jpg',1)
insert into tb_producto values ('PROD000131','LAB0000001',19,'EN01354','GASEOVET 80  mg','MEDIFARMA',900,53.00,'131.jpg',1)
insert into tb_producto values ('PROD000132','LAB0000001',19,'EN01554','GASEOPLUS FRESA 80  mg','HERSIL',700,15.00,'132.jpg',1)
insert into tb_producto values ('PROD000133','LAB0000001',19,'NG0645','GASEOVET 40  mg','MEDIFARMA',700,224.00,'133.jpg',1)
insert into tb_producto values ('PROD000134','LAB0000001',19,'EE04601','AERO OM SABOR A FRESAS 100  mg','OM PHARMA',900,16.00,'134.jpg',1)
insert into tb_producto values ('PROD000135','LAB0000001',19,'ER04601','SIMETICONA  40  mg','INDUSTRIA',700,9.00,'135.jpg',1)

insert into tb_producto values ('PROD000136','LAB0000001',20,'NG1521','SUCRAGANT 1 g/5 mL Suspensión Oral 1 unid','ROXFARMA',900,26.50,'136.jpg',1)
go


/*--------------------------------------------------------------------------------*/
 
create or alter procedure usp_producto_listar
as
begin
	select p.id_producto
      ,p.id_laboratorio
	  ,p.id_tipoprod
      ,tp.nombre_tipoprod
      ,p.codigobar_producto
      ,p.descripcion_producto
      ,p.marca_producto
      ,p.stock_producto
      ,p.preciounid_producto
	  ,p.imagen_producto 
      ,p.estado
	from tb_producto p
	inner join tb_tipoproducto tp on tp.id_tipoprod= p.id_tipoprod
	where p.estado =1
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_producto_agregar
-- @id char(10),
@idlaboratorio char(10),
@idtipo int,
@codigobarras varchar(14),
@descripcion varchar(240),
@marca varchar(12),
@stockproducto int,
@precioproducto float,
@imagenproducto varchar(50)
as
begin
	declare @codigo char(10)
	set @codigo=dbo.autogeneraproducto()
	insert into tb_producto values (@codigo,@idlaboratorio,@idtipo,@codigobarras,@descripcion,@marca,@stockproducto,@precioproducto,@imagenproducto,1)

end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_producto_actualizar
@id char(10),
@idlaboratorio char(10),
@idtipo int,
@codigobarras varchar(14),
@descripcion varchar(240),
@marca varchar(12),
@stockproducto int,
@precioproducto float
as
begin
	update tb_producto set id_laboratorio = id_laboratorio, id_tipoprod = @idtipo, codigobar_producto = @codigobarras, descripcion_producto = @descripcion, marca_producto = @marca, stock_producto=@stockproducto,preciounid_producto=@precioproducto   where id_producto = @id
end
go
/*--------------------------------------------------------------------------------*/

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



/*--------------------------------------------------------------------------------*/


create or alter function dbo.autogenerafacturacion() returns varchar(10)
As
Begin
	Declare @n int
	Declare @cod varchar(10)=(Select top 1 id_facturacion from tb_facturacion order by 1 desc)

	if(@cod is null)
		Set @n=1
	else
		Set @n=CAST(subString(@cod,3,10) as int)+1

	return CONCAT('FAC',REPLICATE('0',7-LEN(@n)),@n)
End
go


/*--------------------------------------------------------------------------------*/

create or alter procedure usp_facturacion_listar
as
begin
	select * from tb_facturacion
end	
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_facturacion_agregar
@idfarmacia char(10),
@fechaemision date,
@subtotal float
as
begin
	declare @codigo char(10)
	set @codigo=dbo.autogenerafacturacion()
	insert into tb_facturacion values (@codigo,@idfarmacia,@fechaemision,@subtotal,1)
end
go
/*--------------------------------------------------------------------------------*/

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

/*--------------------------------------------------------------------------------*/
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


/*--------------------------------------------------------------------------------*/

create or alter procedure usp_detalle_facturacion_listar
as
begin
	select * from tb_detalle_facturacion
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_facturacion_farmacia_producto_listar
as
begin
	select p.descripcion_producto,f.fechaemision_facturacion,fr.ruc_farmacia,fr.razonsocial_farmacia,f.subtotal_facturacion from tb_detalle_facturacion df inner join tb_facturacion f on df.id_facturacion = f.id_facturacion join tb_producto p on df.id_producto = p.id_producto join tb_farmacia fr on f.id_farmacia = fr.id_farmacia
end
go
/*--------------------------------------------------------------------------------*/

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
/*--------------------------------------------------------------------------------*/

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
/*--------------------------------------------------------------------------------*/

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

if OBJECT_ID('tb_orden_compra','U') is null create table tb_orden_compra
(
	id_orden_compra int identity(1,1) primary key,
    id_laboratorio char(10) not null,
	id_producto char(10) not null, 
	nro_orden_compra char(10) unique not null,
	fechaorden_compra date not null,
	fechaentrega date not null,
	condicionespago varchar(25) not null,
	valortotal_orden float not null,
	estado bit not null,
	foreign key (id_laboratorio) references tb_laboratorio(id_laboratorio),
	foreign key (id_producto) references tb_producto(id_producto)
)
go



insert into tb_orden_compra values ('LAB0000001','PROD000001','NRO0000001','2022-10-01','2022-10-03','credito','100.00',1)
go
insert into tb_orden_compra values ('LAB0000001','PROD000002','NRO0000002','2022-09-01','2022-09-03','credito','100.00',1)
go



/*--------------------------------------------------------------------------------*/

create or alter procedure usp_orden_compra_listar
as
begin
	select * from tb_orden_compra
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_orden_compra_agregar
@idlaboratorio char(10),
@idproducto char(10),
@numordencompra char(10),
@fechaordencompra date,
@fechaentrega date,
@condicionespago varchar(25),
@valortotalorden float

as
begin
	insert into tb_orden_compra values (@idlaboratorio,@idproducto,@numordencompra,@fechaordencompra,@fechaentrega,@condicionespago,@valortotalorden,1)
end
go
/*--------------------------------------------------------------------------------*/

create or alter procedure usp_orden_compra_actualizar
@idordencompra int,
@idlaboratorio char(10),
@idproducto char(10),
@numordencompra char(10),
@fechaordencompra date,
@fechaentrega date,
@condicionespago varchar(25),
@valortotalorden float
 
as
begin
	update tb_orden_compra set id_laboratorio=@idlaboratorio,id_producto=@idproducto, nro_orden_compra= @numordencompra, fechaorden_compra=@fechaordencompra, fechaentrega=@fechaentrega, condicionespago=@condicionespago, valortotal_orden=@valortotalorden where id_orden_compra = @idordencompra
end
go

/*--------------------------------------------------------------------------------*/

create or alter procedure usp_orden_compra_eliminar
@idordencompra int
as
begin
	update tb_orden_compra set estado = 0 where id_orden_compra = @idordencompra
end
go

