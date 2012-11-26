select * from squawks s
	inner join (
select UserId, AvatarUrl, UserName, FirstName, LastName from Users where UserId=1
union
select f.followinguserid, uf.AvatarUrl, uf.UserName, uf.FirstName, uf.LastName 
from Followers f 
	inner join Users uf on f.FollowingUserId = uf.UserId
where f.UserId=1 ) h on h.userid = s.userid  

select *, 
	(select count(1)
		from Followers f
	where u.UserId = f.FollowingUserId) Followers, 
	(select count(1) 
		from Followers f
	where u.UserId = f.UserId) Following, 
	(select count(1) 
		from Followers f
			inner join Users u2 on u2.UserId = f.UserId
	where f.FollowingUserId = u.UserId 
	and u2.UserName = 'rob') IsFollowing
from users u 
where username = 'tim'

select * from Users
select * from Followers