alter table dbo.Users add Bio nvarchar(max)
go
create table dbo.Followers (
	UserId int not null,
	FollowingUserId int not null,
	constraint FollowersPk primary key (UserId, FollowingUserId))
go

create unique index Followers_following_idx on dbo.Followers(FollowingUserId, UserId)
go
