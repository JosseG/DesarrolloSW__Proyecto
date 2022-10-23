drop database if exists CyberLab;
-- creamos la bd
create database CyberLab;
-- activamos la bd
use CyberLab;


create table if not exists tb_tipoproducto
(
    id_tipoprod int auto_increment primary key,
    nombre_tipoprod varchar(10) unique  not null,
    estado boolean not null
);


create table if not exists tb_farmacia
(
	id_farmacia char(10) primary key,
	ruc_farmacia char(14) not null unique,
	razonsocial_farmacia varchar(90) not null,
	telefono_farmacia varchar(90) not null,
    direccion_farmacia varchar(90) not null, /*tabla direccion pendiente*/
	estado boolean not null
);

create table if not exists tb_laboratorio
(
	id_laboratorio char(10) primary key,
	ruc_laboratorio char(11) not null unique,
	razonsocial_laboratorio varchar(90) not null,
	telefono_laboratorio varchar(90) not null,
	estado boolean not null
);

create table if not exists tb_producto
(
	id_producto char(10) primary key,
    id_laboratorio char(10) not null,
    id_tipoprod int not null,
	codigobar_producto varchar(14) not null unique,
	descripcion_producto varchar(240) not null,
	marca_producto varchar(12) not null,
	estado boolean not null,
    foreign key(id_tipoprod) references tb_tipoproducto(id_tipoprod),
    foreign key(id_laboratorio) references tb_laboratorio(id_laboratorio)
);

create table if not exists tb_orden_compra
(
	id_orden_compra int auto_increment primary key,
    id_laboratorio char(10) not null,
	nro_orden_compra char(10) unique not null,
	fechaorden_compra date not null,
	fechaentrega date not null,
	condicionespago varchar(25) not null,
	transporte char(20) null,
	valortotal_orden float not null,
	foreign key (id_laboratorio) references tb_laboratorio(id_laboratorio)
);

create table if not exists tb_stock
(
    id_stock int auto_increment primary key,
    id_producto char(10) not null,
    cantidad_stock int not null,   
    preciounitario_stock float not null,
    estado boolean not null,
    foreign key (id_producto) references tb_producto (id_producto)
);

create table if not exists tb_detalleordencompra 
(
	id_detalle_orden int auto_increment primary key,
	id_orden_compra int not null,
	id_producto char(10) not null,
	cantidad int not null,
	precio_unitario float not null,
	foreign key(id_orden_compra) references tb_orden_compra(id_orden_compra),
	foreign key(id_producto) references tb_producto(id_producto)
);


create table if not exists tb_chat
(
id_chat char(10) primary key,
id_farmacia char(10) not null,
id_laboratorio char(10) not null,
mensaje_chat char(255) not null,
fecha_chat datetime not null,
foreign key (id_farmacia) references tb_farmacia(id_farmacia),
foreign key (id_laboratorio) references tb_laboratorio(id_laboratorio)
);

create table if not exists tb_usuario_farmacia
(
	id_usuario_farmacia int auto_increment primary key, 
	id_farmacia char(10) not null,
	alias_usuario_farmacia varchar(30) not null unique,
	contrasena_usuario_farmacia varchar(50) not null,
    estado boolean not null,
	foreign key (id_farmacia) references tb_farmacia(id_farmacia)
);



create table if not exists tb_facturacion 
(
	id_facturacion char(10) primary key,
    producto_facturacion varchar(60) not null ,
	fechaemision_facturacion date not null,
	rucfarmacia_facturacion char(11) not null references tb_farmacia,
    razonsocial_facturacion varchar(90) not null,
    subtotal_facturacion float not null,
    estado boolean not null
);

create table if not exists tb_producto_farmacia_facturacion
(
id_producto char(10) not null,
id_farmacia char(10) not null,
id_facturacion char(10) not null,
estado boolean not null,
primary key(id_producto,id_farmacia,id_facturacion),
foreign key(id_producto) references tb_producto(id_producto),
foreign key(id_farmacia) references tb_farmacia(id_farmacia),
foreign key(id_facturacion) references tb_facturacion(id_facturacion)
);

