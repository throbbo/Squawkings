namespace Squawkings.Models
{
	public class GlobalSquawk : TemplateBuilder
	{
		public GlobalSquawk()
		{
			Template1 =
				AddTemplate(@"select * from squawks s 
	inner join Users u on u.UserId = s.UserId
where /**where**/
 order by CreatedAt desc");
		}
	}
}