create database "APP-NET-CORE"

create table Usuarios(
id int identity (1001,1) primary key,
nombre varchar(50) not null,
usuario varchar(50) not null,
clave varchar(500) not null,
correo varchar(50)not null,
id_rol int not null,
url_foto_perfil varchar(200)
)

create table Roles(
id int identity (1,1) primary key,
rol varchar(50) not null,
descripcion varchar(100) not null,
url_foto_rol varchar(200)
)

create table Personas(
id int identity (1,1) primary key,
cedula varchar(50) not null,
nombre varchar(50),
apellido varchar(50),
telefono varchar(50),
whatsapp varchar(50),
ciudad varchar(50),
sector varchar(50),
circ varchar(50),
email varchar
)

create proc sp_RegistrarUsuario(
@nombre varchar(50),
@usuario varchar(50),
@clave varchar(500),
@correo varchar(50),
@id_rol int,
@url_foto_perfil varchar(200),
@Registrado bit output,
@Mensaje varchar(100) output
)
as
begin
	if(not exists(select * from Usuarios where correo=@correo))
	begin
		insert into Usuarios values (@nombre, @usuario, @clave, @correo, @id_rol, @url_foto_perfil)
		set @Registrado = 1
		set @Mensaje = 'Usuario registrado'
	end
	else
	begin
		set @Registrado = 0
		set @Mensaje = 'Ya existe un usuario con este correo'
	end
end

create proc sp_ValidarUsuario(
@usuario varchar(50),
@clave varchar(500)
)
as
begin
	if(exists(select * from Usuarios where usuario=@usuario and clave=@clave))
	begin
		select id from Usuarios where usuario=@usuario and clave=@clave
	end
	else
	begin
		select '0'
	end
end

declare @Registrado bit, @Mensaje varchar(100)
exec sp_RegistrarUsuario 'Anderson Sosa', 'adsosa', 'AE30CB3ACD09F1C95191459883B724154A5FBE7B8455A880DC602DE73EB0580E', 'a.davidsosa.t@gmail.com', 1, null, @Registrado output, @Mensaje output

select @Registrado, @Mensaje