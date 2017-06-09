using System.Reflection;
using System.Web;
using Demo.WebApi;

[assembly: AssemblyTitle("Demo.WebApi")]
[assembly: AssemblyProduct("Demo.WebApi")]
[assembly: PreApplicationStartMethod(typeof(Startup), nameof(Startup.Start))]
