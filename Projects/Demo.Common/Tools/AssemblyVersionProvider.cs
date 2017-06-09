namespace Demo.Common.Tools
{
    using System;
    using System.Reflection;
    using Interfaces;

    public sealed class AssemblyVersionProvider : IAssemblyVersionProvider
    {
        public Version Get(Assembly assembly)
        {
            return assembly.GetName().Version;
        }
    }
}
