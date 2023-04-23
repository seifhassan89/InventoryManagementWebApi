
using System.Reflection;

namespace StoreWebAPI
{
    public class ConfigureService
    {

        public static void RegisterRepositories(IServiceCollection servicesCollection)
        {
            List<Type> repoAssemblyTypes = Assembly.Load("Repositories").ExportedTypes.Where(a => a.Name.ToLower().EndsWith("repository")).ToList();
            //get service interfaces
            List<Type> repoInterfaces = repoAssemblyTypes.Where(a => a.IsInterface).ToList();
            //get service implementation
            List<Type> repoImplementations = repoAssemblyTypes.Where(a => a.IsClass).ToList();
            RegisterTypes(servicesCollection, repoInterfaces, repoImplementations);
        }

        /// <summary>
        /// Register Services Assembly
        /// </summary>
        /// <param name="servicesCollection"></param>
        public static void RegisterServices(IServiceCollection servicesCollection)
        {
            List<Type> serviceAssemblyTypes = Assembly.Load("Services").ExportedTypes.Where(a => a.Name.ToLower().EndsWith("service")).ToList();
            //get service interfaces
            List<Type> serviceInterfaces = serviceAssemblyTypes.Where(a => a.IsInterface).ToList();
            //get service implementation
            List<Type> serviceImplementations = serviceAssemblyTypes.Where(a => a.IsClass).ToList();
            RegisterTypes(servicesCollection, serviceInterfaces, serviceImplementations);
        }

        /// <summary>
        /// Register Mappers
        /// </summary>
        /// <param name="servicesCollection"></param>
        public static void RegisterMappers(IServiceCollection servicesCollection)
        {
            List<Type> serviceAssemblyTypes = Assembly.Load("Mappers").ExportedTypes.Where(a => a.Name.ToLower().EndsWith("mapper")).ToList();
            //get service interfaces
            List<Type> serviceInterfaces = serviceAssemblyTypes.Where(a => a.IsInterface).ToList();
            //get service implementation
            List<Type> serviceImplementations = serviceAssemblyTypes.Where(a => a.IsClass).ToList();
            RegisterTypes(servicesCollection, serviceInterfaces, serviceImplementations);
        }
        /// <summary>
		/// Register Types
		/// </summary>
		/// <param name="servicesCollection"></param>
		/// <param name="interfaces"></param>
		/// <param name="implementations"></param>
		private static void RegisterTypes(IServiceCollection servicesCollection, List<Type> interfaces, List<Type> implementations)
        {
            foreach (Type interfaceType in interfaces)
            {
                Type serviceType = implementations.FirstOrDefault(imp => interfaceType.IsAssignableFrom(imp));
                if (serviceType != null)
                {
                    servicesCollection.AddScoped(interfaceType, serviceType);
                }
            }
        }
    }
}
