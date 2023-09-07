using FileServer.Model.Entities;
using FileServer.Model.Helpers;
using FileServer.Model.Interfaces;
using FileServer.Model.Services;

namespace FileServer.Model.Extentions
{
    public static class AddFileComponentsExtention
    {
        public static IServiceCollection AddFileComponents(this IServiceCollection services, ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            PathConfiguration pathConfiguration = new PathConfiguration(
                environment.WebRootPath,
                configuration.GetSection("PathConfiguration")["ProductSubpath"],
                configuration.GetSection("PathConfiguration")["ProductMainImageName"]
                );
            services.AddSingleton<PathConfiguration>(pathConfiguration);

            services.AddSingleton<IProductPathsHelper>(new ProductPathsHelper(pathConfiguration));

            services.AddSingleton<IFileService>(new FileService());

            return services;
        }
    }
}
