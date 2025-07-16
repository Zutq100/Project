using System.Reflection;

namespace Application
{
    public class AssemblyReference
    {
        public static readonly Assembly Assemblies = typeof(AssemblyReference).Assembly;
    }
}
