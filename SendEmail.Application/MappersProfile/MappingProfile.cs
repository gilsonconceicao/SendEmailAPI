using AutoMapper;
using System.Reflection;

namespace SendEmail.Application.MappersProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        ApplyCustomMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });

        }
    }

    private void ApplyCustomMappingsFromAssembly(Assembly assembly)
    {

        var types = assembly.DefinedTypes.Where(i => i.ImplementedInterfaces.Contains(typeof(ICustomMap)));
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("CustomMap");

            methodInfo?.Invoke(instance, new object[] { this });

        }
    }
}
