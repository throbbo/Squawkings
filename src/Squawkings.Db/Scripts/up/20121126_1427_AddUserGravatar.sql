alter table dbo.Users add IsGravatar tinyint
go
update dbo.Users set IsGravatar=0
go
 