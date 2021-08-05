insert into [Role] ([Name], Slug) values ('Author', 'author')

insert into Tag ([Name], Slug) values ('ASP.NET', 'aspnet')

insert into [User] 
(Name,Email,PasswordHash,Bio,Image,Slug) values
('jesiel', 'jesiel@gmail.com', 'hash', 'teste', 'https://', 'jesiel-padilha')


insert into [UserRole] (UserId, RoleId) values (1,1) 