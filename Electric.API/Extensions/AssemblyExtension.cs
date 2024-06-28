using System.Reflection;

namespace Electric.API.Extensions
{
    /// <summary>
    /// Assembly扩展
    /// </summary>
    public static class AssemblyExtension
    {
        /// <summary>
        /// 获取所有程序集
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static List<Assembly> GetReferanceAssemblies(this AppDomain domain)
        {
            var list = new List<Assembly>();
            domain.GetAssemblies().ToList().ForEach(i =>
            {
                GetReferanceAssemblies(i, list);
            });
            return list;
        }
        static void GetReferanceAssemblies(Assembly assembly, List<Assembly> list)
        {
            assembly.GetReferencedAssemblies().ToList().ForEach(i =>
            {
                var ass = Assembly.Load(i);
                if (!list.Contains(ass))
                {
                    list.Add(ass);
                    GetReferanceAssemblies(ass, list);
                }
            });
        }
    }
}
