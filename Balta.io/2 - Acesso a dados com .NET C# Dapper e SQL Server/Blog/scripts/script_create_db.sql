create database Blog
go
use Blog
go
create table [User] (
	Id int not null identity(1,1),
	Name varchar(80) not null,
	Email varchar(200) not null,
	PasswordHash varchar(255) not null,
	Bio text not null,
	Image varchar(2000) not null,
	Slug varchar(80) not null,

	constraint PK_User primary key(Id),
	constraint UQ_User_Email unique(Email),
	constraint UQ_User_Slug unique(Slug)
)
create nonclustered index IX_User_Email ON [User](Email)
create nonclustered index IX_User_Slug ON [User](Slug)
go
create table Role (
	Id int not null identity(1,1),
	Name varchar(80) not null,
	Slug varchar(80) not null,

	constraint PK_Role primary key(Id),
	constraint UQ_Role_Slug unique(Slug)
)
create nonclustered index IX_Role_Slug ON [Role](Slug)
go
create table UserRole (
	UserId int not null,
	RoleId int not null,

	constraint PK_UserRole primary key(UserId, RoleId),
)
go
create table Category (
	Id int not null identity(1,1),
	Name varchar(80) not null,
	Slug varchar(80) not null,

	constraint PK_Category primary key(Id),
	constraint UQ_Category_Slug unique(Slug)
)
create nonclustered index IX_Cateogry_Slug ON [Category](Slug)
go
create table Post(
	Id int not null identity(1,1),
	CategoryId int not null,
	AuthorId int not null,
	Title varchar(160) not null,
	Summary varchar(255) not null,
	Body text not null,
	Slug varchar(80) not null,
	CreateDate datetime not null default(getdate()),
	LastUpdateDate datetime not null default(getdate()),

	constraint PK_Post primary key(Id),
	constraint FK_Post_Category foreign key (CategoryId) references Category(Id),
	constraint FK_Post_Author foreign key (AuthorId) references [User](Id),
	constraint UQ_Post_Slug unique(Slug)
)
create nonclustered index IX_Post_Slug on Post(Slug)
go
create table Tag (
	Id int not null identity(1,1),
	Name varchar(80) not null,
	Slug varchar(80) not null,

	constraint PK_Tag primary key(Id),
	constraint UQ_Tag_Slug unique(Slug)
)
create nonclustered index IX_Tag_Slug ON [Tag](Slug)
go
create table PostTag (
	PostId int not null,
	TagId int not null,

	constraint PK_PostTag primary key(PostId, TagId),
)
