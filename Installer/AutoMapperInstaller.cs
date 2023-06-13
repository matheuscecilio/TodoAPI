using System.Reflection;

namespace TodoAPI.Installer;

public static class AutoMapperInstaller
{
    public static void InstallAutoMapper(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(executingAssembly);
    }
}
