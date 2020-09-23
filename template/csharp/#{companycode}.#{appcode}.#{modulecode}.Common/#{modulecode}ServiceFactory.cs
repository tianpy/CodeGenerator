using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #{companycode}.#{appcode}.#{modulecode}.Common
{
    using System.Reflection;
    using Teld.BIZ.Common.Kernel;

    public class #{modulecode}ServiceFactory<T> where T : class
    {
        public static T GetService()
        {
#if DEBUG
            var typeName = typeof(T).FullName.Replace("SPI", "Service").Replace(typeof(T).Name, typeof(T).Name.Replace("I", ""));
            var assemblyName = typeof(T).Namespace.Replace("SPI", "Service");
            Assembly.Load(assemblyName);
        
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(T))))
                                .ToArray();
            foreach (var v in types)
            {
                if (v.IsClass)
                {
                    return Activator.CreateInstance(v) as T;
                }
            }

            return null;
#else
                        return ServiceFactory<T>.GetService();

#endif     

        }
    }
}
