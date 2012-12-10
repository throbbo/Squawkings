using NPoco;
using StructureMap;
namespace Squawkings {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                        	//x.For<IDatabase>().HybridHttpOrThreadLocalScoped().Use(() => new Database("Squawkings"));
							x.For<IDatabase>().HybridHttpOrThreadLocalScoped().Use(GetDatabaseFactory.GetDatabase);
                        });
            return ObjectFactory.Container;
        }
    }

	public class GetDatabaseFactory
	{
		public static IDatabase GetDatabase ()
		{
			return new Database("Squawkings");
		}
	}
}